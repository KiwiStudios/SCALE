using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.String;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace InputMapSourceGenerator;

[Generator]
#pragma warning disable RS1036
public class GenerateInputMapSourceGenerator : ISourceGenerator
#pragma warning restore RS1036
{
    public void Execute(GeneratorExecutionContext context)
    {
        // NOTE(thomas): To debug this, use the following:
        // 1. https://blog.jetbrains.com/dotnet/2023/07/13/debug-source-generators-in-jetbrains-rider/#DebuggingaSourceGenerator
        // 2. https://github.com/JoanComasFdz/dotnet-how-to-debug-source-generator-vs2022

        // https://roslynquoter.azurewebsites.net/
        var receiver = (MainSyntaxReceiver)context.SyntaxReceiver!;

        var generateMethodCaptures = receiver?.GenerateClassForActionsAggregate.Captures;
        var namespaceCapture = receiver?.GenerateClassForActionsAggregate.Namespace;

        if (generateMethodCaptures is null || namespaceCapture is null)
        {
            return;
        }

        var lines = File.ReadAllLines(receiver?.GenerateClassForActionsAggregate.GodotProjectFilePath!);
        var actions = GetActionsFromProjectFile(lines);
        var fields = new List<FieldDeclarationSyntax>();

        foreach (var action in actions)
        {
            fields.Add(CreateFieldForAction(action));
        }

        // https://roslynquoter.azurewebsites.net/
        var output = CompilationUnit()
            // .WithUsings(SingletonList<UsingDirectiveSyntax>(
            //     UsingDirective(
            //         IdentifierName("Godot"))))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    namespaceCapture
                        .WithMembers(
                            SingletonList<MemberDeclarationSyntax>(
                                ClassDeclaration("InputMappings")
                                    .WithModifiers(
                                        TokenList(
                                            new[]
                                            {
                                                Token(SyntaxKind.PublicKeyword),
                                                Token(SyntaxKind.PartialKeyword)
                                            }))
                                    .WithMembers(new SyntaxList<MemberDeclarationSyntax>(fields))
                            ))
                )
            )
            .NormalizeWhitespace();

        context.AddSource("InputMappings.g.cs", output.ToFullString().ToString());
    }

    private FieldDeclarationSyntax CreateFieldForAction(string action)
    {
        return FieldDeclaration(
                VariableDeclaration(
                        PredefinedType(
                            Token(SyntaxKind.StringKeyword)))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                    Identifier(action))
                                .WithInitializer(
                                    EqualsValueClause(
                                        LiteralExpression(
                                            SyntaxKind.StringLiteralExpression,
                                            Literal(action)))))))
            .WithModifiers(
                TokenList(
                    new[]
                    {
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.StaticKeyword)
                    }));
    }

    private List<string> GetActionsFromProjectFile(IEnumerable<string> lines)
    {
        var actions = new List<string>();
        var matchingInput = false;
        // This text parser is shit. Maybe we can use Godot's parsing methods for parsing project.godot files?
        // It's good enough for now.
        foreach (var line in lines.Where(x => x != Empty))
        {
            if (line is "[input]")
            {
                matchingInput = true;
                continue;
            }

            if (matchingInput && line.Contains("={"))
            {
                actions.Add(line.Substring(0, line.IndexOf("={", StringComparison.Ordinal)));
            }
        }

        return actions;
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver());
    }
}

public class MainSyntaxReceiver : ISyntaxReceiver
{
    public GenerateClassForActionsAggregate GenerateClassForActionsAggregate { get; set; } =
        new GenerateClassForActionsAggregate();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        GenerateClassForActionsAggregate.OnVisitSyntaxNode(syntaxNode);
    }
}

public class GenerateClassForActionsAggregate : ISyntaxReceiver
{
    public List<Capture> Captures { get; set; } = new List<Capture>();
    public FileScopedNamespaceDeclarationSyntax? Namespace { get; set; } = null;

    public string GodotProjectFilePath = string.Empty;

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        var dir = Path.Join(Path.GetDirectoryName(syntaxNode.SyntaxTree.GetLineSpan(syntaxNode.Span).Path),
            "project.godot");
        if (File.Exists(dir) && GodotProjectFilePath == Empty)
        {
            GodotProjectFilePath = dir;
        }

        if (syntaxNode is AttributeSyntax
            {
                Name: IdentifierNameSyntax { Identifier.Text: "GenerateInputMappings" }
            } clss)
        {
            var dds = clss.GetParent<ClassDeclarationSyntax>();
            Namespace ??= dds.GetParent<FileScopedNamespaceDeclarationSyntax>();
        }
    }

    public class Capture
    {
        public string Key { get; }
        public DelegateDeclarationSyntax DelegateDeclarationSyntax { get; }

        public Capture(string key, DelegateDeclarationSyntax delegateDeclarationSyntax)
        {
            Key = key;
            DelegateDeclarationSyntax = delegateDeclarationSyntax;
        }
    }
}