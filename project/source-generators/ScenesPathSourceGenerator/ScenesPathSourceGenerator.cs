using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.String;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace ScenesPathSourceGenerator;

[Generator]
#pragma warning disable RS1036
public class ScenesPathSourceGenerator : ISourceGenerator
#pragma warning restore RS1036
{
    private record SceneFilePath(string FieldName,
        string RelativeFilePath,
        string FileExtension);

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

        var sceneFilePaths = Directory.GetFiles(receiver!.GenerateClassForActionsAggregate.SceneFolderPath, "*.*",
            SearchOption.AllDirectories);
        var filePathsRelative = TrimAbsoluteFromFilePaths(sceneFilePaths,
            receiver.GenerateClassForActionsAggregate.GodotProjectDirectoryPath!);

        var fields = new List<FieldDeclarationSyntax>();

        foreach (var filePath in filePathsRelative)
        {
            fields.Add(CreateFieldForScenePath(filePath));
        }

        // https://roslynquoter.azurewebsites.net/
        var output = CompilationUnit()
            .WithUsings(SingletonList<UsingDirectiveSyntax>(
                UsingDirective(
                    IdentifierName("Godot"))))
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    namespaceCapture
                        .WithMembers(
                            SingletonList<MemberDeclarationSyntax>(
                                ClassDeclaration(receiver.GenerateClassForActionsAggregate.ClassDeclaration!.Identifier)
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

        context.AddSource($"{receiver.GenerateClassForActionsAggregate.ClassDeclaration.Identifier}.g.cs",
            output.ToFullString().ToString());
    }

    private string MapFileExtensionToGodotSceneClass(string fileExtension)
    {
        return fileExtension switch
        {
            ".tscn" => "PackedScene",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension,
                "Could not parse file extension to godot scene file")
        };
    }

    private List<SceneFilePath> TrimAbsoluteFromFilePaths(string[] sceneFilePaths, string godotProjectDirectoryPath)
    {
        var sceneFileExtensions = new List<string>()
        {
            ".tscn",
        };

        var paths = new List<SceneFilePath>();
        foreach (var filePath in sceneFilePaths)
        {
            var path = Path.GetRelativePath(godotProjectDirectoryPath, filePath);
            var ext = Path.GetExtension(path);

            if (sceneFileExtensions.Contains(ext))
            {
                // E.g.
                // Scenes\Menu\menu.tscn
                paths.Add(
                    new SceneFilePath(
                        path
                            .Replace($"Scenes{Path.DirectorySeparatorChar}", "")
                            .Replace(Path.DirectorySeparatorChar, '_')
                            .Replace("-", "_")
                            .Replace(" ", "_")
                            .Replace(".", "_")
                            .Replace("TSCN", "SCENE", StringComparison.InvariantCultureIgnoreCase)
                            .ToUpperInvariant(),
                        path
                            .Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar),
                        ext
                    )
                );
            }
        }

        return paths;
    }

    private FieldDeclarationSyntax CreateFieldForScenePath(SceneFilePath sceneFilePath)
    {
        return FieldDeclaration(
                VariableDeclaration(
                        IdentifierName(MapFileExtensionToGodotSceneClass(sceneFilePath.FileExtension)))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                    Identifier(sceneFilePath.FieldName))
                                .WithInitializer(
                                    EqualsValueClause(
                                        InvocationExpression(
                                                MemberAccessExpression(
                                                    SyntaxKind.SimpleMemberAccessExpression,
                                                    IdentifierName("ResourceLoader"),
                                                    GenericName(
                                                            Identifier("Load"))
                                                        .WithTypeArgumentList(
                                                            TypeArgumentList(
                                                                SingletonSeparatedList<TypeSyntax>(
                                                                    IdentifierName(
                                                                        MapFileExtensionToGodotSceneClass(sceneFilePath
                                                                            .FileExtension)))))))
                                            .WithArgumentList(
                                                ArgumentList(
                                                    SingletonSeparatedList<ArgumentSyntax>(
                                                        Argument(
                                                            LiteralExpression(
                                                                SyntaxKind.StringLiteralExpression,
                                                                Literal(
                                                                    $"res://{sceneFilePath.RelativeFilePath}")))))))))))
            .WithModifiers(
                TokenList(
                    new[]
                    {
                        Token(SyntaxKind.PublicKeyword),
                        Token(SyntaxKind.StaticKeyword)
                    }));
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

    public string SceneFolderPath = string.Empty;
    public string GodotProjectFilePath = string.Empty;
    public string? GodotProjectDirectoryPath = string.Empty;
    public ClassDeclarationSyntax? ClassDeclaration;

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        var dir = Path.Join(Path.GetDirectoryName(syntaxNode.SyntaxTree.GetLineSpan(syntaxNode.Span).Path),
            "project.godot");
        if (File.Exists(dir) && SceneFolderPath == Empty)
        {
            GodotProjectFilePath = dir;
            GodotProjectDirectoryPath = Path.GetDirectoryName(syntaxNode.SyntaxTree.GetLineSpan(syntaxNode.Span).Path);
            SceneFolderPath = Path.Join(GodotProjectDirectoryPath, "Scenes");
        }

        if (syntaxNode is AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "GenerateScenePaths" } } clss)
        {
            var dds = clss.GetParent<ClassDeclarationSyntax>();
            ClassDeclaration = dds;
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