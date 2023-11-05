using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace EventBusSourceGenerator;

[Generator]
#pragma warning disable RS1036
public class GenerateMethodForEventHandlerSourceGenerator : ISourceGenerator
#pragma warning restore RS1036
{
    public void Execute(GeneratorExecutionContext context)
    {
        // NOTE(thomas): To debug this, use the following:
        // 1. double shift -> attach to process..
        // 2. Attach to VBCSCompiler.dll to debug 

        // https://roslynquoter.azurewebsites.net/
        var receiver = (MainSyntaxReceiver)context.SyntaxReceiver!;

        var generateMethodCaptures = receiver?.GenerateMethodForEventHandlerAggregate.Captures;
        var namespaceCapture = receiver?.GenerateMethodForEventHandlerAggregate.Namespace;

        if (generateMethodCaptures is null || namespaceCapture is null)
        {
            return;
        }

        var methods = new List<MethodDeclarationSyntax>();

        if (generateMethodCaptures?.Count > 0)
        {
            foreach (var generateMethodCapture in generateMethodCaptures)
            {
                var qns = namespaceCapture.GetChild<QualifiedNameSyntax>();

                var parameterList = generateMethodCapture.DelegateDeclarationSyntax.ParameterList;
                var methodName = generateMethodCapture.DelegateDeclarationSyntax.Identifier.ValueText;
                var eventName = methodName.Replace("EventHandler", "");

                var parentClass = generateMethodCapture.DelegateDeclarationSyntax.GetParent<ClassDeclarationSyntax>();
                var inheritDoc =
                    $"/// <inheritdoc cref=\"global::{qns?.Left}.{qns?.Right}.{parentClass.Identifier}.{methodName}\"/>";

                methods.Add(CreateEventHandler(parameterList, eventName, generateMethodCapture.CommentTrivia,
                    inheritDoc));
            }
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
                                ClassDeclaration("EventBus")
                                    .WithModifiers(
                                        TokenList(
                                            new[]
                                            {
                                                Token(
                                                    TriviaList(
                                                        Trivia(
                                                            NullableDirectiveTrivia(
                                                                Token(SyntaxKind.EnableKeyword),
                                                                true))),
                                                    SyntaxKind.PublicKeyword,
                                                    TriviaList()),
                                                Token(SyntaxKind.PartialKeyword),
                                            }))
                                    .WithBaseList(
                                        BaseList(
                                            SingletonSeparatedList<BaseTypeSyntax>(
                                                SimpleBaseType(
                                                    IdentifierName("Godot.Node")))))
                                    .WithMembers(new SyntaxList<MemberDeclarationSyntax>(methods))
                            ))
                )
            )
            .NormalizeWhitespace();

        context.AddSource("EventBus.g.cs", output.ToFullString().ToString());
    }

    public MethodDeclarationSyntax CreateEventHandler(ParameterListSyntax parameterListSyntax,
        string eventName,
        SyntaxTrivia? commentTrivia,
        string doc)
    {
        var parametersToPassToEmitSignal = new List<SyntaxNodeOrToken>()
        {
            Argument(
                LiteralExpression(
                    SyntaxKind.StringLiteralExpression,
                    Literal(eventName)))
        };

        if (parameterListSyntax.Parameters.Any())
        {
            parametersToPassToEmitSignal.Add(Token(SyntaxKind.CommaToken));
        }

        var args = parameterListSyntax.Parameters.Select(x => (SyntaxNodeOrToken)Argument(
            IdentifierName(x.Identifier.Text)));

        for (var i = 0; i < args.Count(); i++)
        {
            parametersToPassToEmitSignal.Add(args.ToArray()[i]);

            if (i != args.Count() - 1)
            {
                parametersToPassToEmitSignal.Add(Token(SyntaxKind.CommaToken));
            }
        }

        return MethodDeclaration(
                PredefinedType(
                    Token(SyntaxKind.VoidKeyword)),
                Identifier($"Emit{eventName}"))
            .WithModifiers(
                TokenList(
                    Token(
                        TriviaList(
                            new[]
                            {
                                Comment(commentTrivia.ToString() ?? ""),
                                Comment(doc)
                            }),
                        SyntaxKind.PublicKeyword,
                        TriviaList())
                ))
            .WithParameterList(
                parameterListSyntax)
            .WithBody(
                Block(
                    IfStatement(
                        IsPatternExpression(
                            IdentifierName($"backing_{eventName}"),
                            UnaryPattern(
                                ConstantPattern(
                                    LiteralExpression(
                                        SyntaxKind.NullLiteralExpression)))),
                        Block(
                            LocalDeclarationStatement(
                                VariableDeclaration(
                                        IdentifierName(
                                            Identifier(
                                                TriviaList(),
                                                SyntaxKind.VarKeyword,
                                                "var",
                                                "var",
                                                TriviaList())))
                                    .WithVariables(
                                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                                            VariableDeclarator(
                                                    Identifier("list"))
                                                .WithInitializer(
                                                    EqualsValueClause(
                                                        InvocationExpression(
                                                            MemberAccessExpression(
                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                IdentifierName($"backing_{eventName}"),
                                                                IdentifierName("GetInvocationList")))))))),
                            ForStatement(
                                    Block(
                                        LocalDeclarationStatement(
                                            VariableDeclaration(
                                                    IdentifierName(
                                                        Identifier(
                                                            TriviaList(),
                                                            SyntaxKind.VarKeyword,
                                                            "var",
                                                            "var",
                                                            TriviaList())))
                                                .WithVariables(
                                                    SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                        VariableDeclarator(
                                                                Identifier(
                                                                    TriviaList(),
                                                                    SyntaxKind.MethodKeyword,
                                                                    "method",
                                                                    "method",
                                                                    TriviaList()))
                                                            .WithInitializer(
                                                                EqualsValueClause(
                                                                    ElementAccessExpression(
                                                                            IdentifierName("list"))
                                                                        .WithArgumentList(
                                                                            BracketedArgumentList(
                                                                                SingletonSeparatedList<ArgumentSyntax>(
                                                                                    Argument(
                                                                                        IdentifierName(
                                                                                            "index")))))))))),
                                        IfStatement(
                                            BinaryExpression(
                                                SyntaxKind.LogicalAndExpression,
                                                IsPatternExpression(
                                                    ConditionalAccessExpression(
                                                        IdentifierName(
                                                            Identifier(
                                                                TriviaList(),
                                                                SyntaxKind.MethodKeyword,
                                                                "method",
                                                                "method",
                                                                TriviaList())),
                                                        MemberBindingExpression(
                                                            IdentifierName("Target"))),
                                                    UnaryPattern(
                                                        ConstantPattern(
                                                            LiteralExpression(
                                                                SyntaxKind.NullLiteralExpression)))),
                                                IsPatternExpression(
                                                    InvocationExpression(
                                                            IdentifierName("IsInstanceValid"))
                                                        .WithArgumentList(
                                                            ArgumentList(
                                                                SingletonSeparatedList<ArgumentSyntax>(
                                                                    Argument(
                                                                        CastExpression(
                                                                            NullableType(
                                                                                IdentifierName("GodotObject")),
                                                                            MemberAccessExpression(
                                                                                SyntaxKind.SimpleMemberAccessExpression,
                                                                                IdentifierName(
                                                                                    Identifier(
                                                                                        TriviaList(),
                                                                                        SyntaxKind.MethodKeyword,
                                                                                        "method",
                                                                                        "method",
                                                                                        TriviaList())),
                                                                                IdentifierName("Target"))))))),
                                                    ConstantPattern(
                                                        LiteralExpression(
                                                            SyntaxKind.FalseLiteralExpression)))),
                                            Block(
                                                SingletonList<StatementSyntax>(
                                                    ExpressionStatement(
                                                        AssignmentExpression(
                                                            SyntaxKind.SubtractAssignmentExpression,
                                                            IdentifierName($"{eventName}"),
                                                            CastExpression(
                                                                NullableType(
                                                                    IdentifierName($"{eventName}EventHandler")),
                                                                IdentifierName(
                                                                    Identifier(
                                                                        TriviaList(),
                                                                        SyntaxKind.MethodKeyword,
                                                                        "method",
                                                                        "method",
                                                                        TriviaList()))))))))))
                                .WithDeclaration(
                                    VariableDeclaration(
                                            IdentifierName(
                                                Identifier(
                                                    TriviaList(),
                                                    SyntaxKind.VarKeyword,
                                                    "var",
                                                    "var",
                                                    TriviaList())))
                                        .WithVariables(
                                            SingletonSeparatedList<VariableDeclaratorSyntax>(
                                                VariableDeclarator(
                                                        Identifier("index"))
                                                    .WithInitializer(
                                                        EqualsValueClause(
                                                            LiteralExpression(
                                                                SyntaxKind.NumericLiteralExpression,
                                                                Literal(0)))))))
                                .WithCondition(
                                    BinaryExpression(
                                        SyntaxKind.LessThanExpression,
                                        IdentifierName("index"),
                                        MemberAccessExpression(
                                            SyntaxKind.SimpleMemberAccessExpression,
                                            IdentifierName("list"),
                                            IdentifierName("Length"))))
                                .WithIncrementors(
                                    SingletonSeparatedList<ExpressionSyntax>(
                                        PostfixUnaryExpression(
                                            SyntaxKind.PostIncrementExpression,
                                            IdentifierName("index")))))),
                    ExpressionStatement(
                        InvocationExpression(
                                IdentifierName("EmitSignal"))
                            .WithArgumentList(
                                ArgumentList(
                                    SeparatedList<ArgumentSyntax>(parametersToPassToEmitSignal
                                    ))))));
    }

    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new MainSyntaxReceiver());
    }
}

public class MainSyntaxReceiver : ISyntaxReceiver
{
    public GenerateMethodForEventHandlerAggregate GenerateMethodForEventHandlerAggregate { get; set; } =
        new GenerateMethodForEventHandlerAggregate();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        GenerateMethodForEventHandlerAggregate.OnVisitSyntaxNode(syntaxNode);
    }
}

public class GenerateMethodForEventHandlerAggregate : ISyntaxReceiver
{
    public List<Capture> Captures { get; set; } = new List<Capture>();
    public FileScopedNamespaceDeclarationSyntax? Namespace { get; set; } = null;

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is AttributeSyntax
            {
                Name: IdentifierNameSyntax { Identifier.Text: "GenerateMethodForEventHandler" }
            } attr)
        {
            var dds = attr.GetParent<DelegateDeclarationSyntax>();

            var commentTrivia = dds.GetLeadingTrivia()
                .FirstOrDefault(trivia => trivia.IsKind(SyntaxKind.SingleLineCommentTrivia) ||
                                          trivia.IsKind(SyntaxKind.MultiLineCommentTrivia));


            Namespace ??= dds.GetParent<FileScopedNamespaceDeclarationSyntax>();

            Captures.Add(new Capture(dds.Identifier.Text, dds, commentTrivia));
        }
    }

    public class Capture
    {
        public SyntaxTrivia? CommentTrivia { get; }
        public string Key { get; }
        public DelegateDeclarationSyntax DelegateDeclarationSyntax { get; }


        public Capture(string key,
            DelegateDeclarationSyntax delegateDeclarationSyntax,
            SyntaxTrivia? commentTrivia)
        {
            if (!string.IsNullOrWhiteSpace(commentTrivia
                    .ToString()
                    ?.Trim()))
            {
                CommentTrivia = commentTrivia;
            }

            Key = key;
            DelegateDeclarationSyntax = delegateDeclarationSyntax;
        }
    }
}