using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.String;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace AudioPathsSourceGenerator;

[Generator]
#pragma warning disable RS1036
public class AudioPathsSourceGenerator : ISourceGenerator
#pragma warning restore RS1036
{
    private record AudioFilePath(string FieldName,
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

        var audioFilePaths = Directory.GetFiles(receiver!.GenerateClassForActionsAggregate.AudioFolderPath, "*.*",
            SearchOption.AllDirectories);
        var audioFilePathsRelative = TrimAbsoluteFromFilePaths(audioFilePaths,
            receiver.GenerateClassForActionsAggregate.GodotProjectDirectoryPath!);

        var fields = new List<FieldDeclarationSyntax>();

        foreach (var audioFilePath in audioFilePathsRelative)
        {
            fields.Add(CreateFieldForAudioPath(audioFilePath));
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

    private string MapFileExtensionToGodotAudioStreamClass(string fileExtension)
    {
        return fileExtension switch
        {
            ".wav" => "AudioStreamWav",
            ".mp3" => "AudioStreamMP3",
            ".ogg" => "AudioStreamOggVorbis",
            _ => throw new ArgumentOutOfRangeException(nameof(fileExtension), fileExtension,
                "Could not parse file extension to godot audio stream")
        };
    }

    private List<AudioFilePath> TrimAbsoluteFromFilePaths(string[] audioFilePaths, string godotProjectDirectoryPath)
    {
        var audioFileExtensions = new List<string>()
        {
            ".wav",
            ".mp3",
            ".ogg",
        };

        var paths = new List<AudioFilePath>();
        foreach (var audioFilePath in audioFilePaths)
        {
            var path = Path.GetRelativePath(godotProjectDirectoryPath, audioFilePath);
            var ext = Path.GetExtension(path);

            if (audioFileExtensions.Contains(ext))
            {
                // E.g.
                // Sounds\Button\success.wav
                // => Button_Success_Wav
                paths.Add(
                    new AudioFilePath(
                        path
                            .Replace($"Sounds{Path.DirectorySeparatorChar}", "")
                            .Replace(Path.DirectorySeparatorChar, '_')
                            .Replace("-", "_")
                            .Replace(" ", "_")
                            .Replace(".", "_")
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

    private FieldDeclarationSyntax CreateFieldForAudioPath(AudioFilePath audioFilePath)
    {
        return FieldDeclaration(
                VariableDeclaration(
                        IdentifierName(MapFileExtensionToGodotAudioStreamClass(audioFilePath.FileExtension)))
                    .WithVariables(
                        SingletonSeparatedList<VariableDeclaratorSyntax>(
                            VariableDeclarator(
                                    Identifier(audioFilePath.FieldName))
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
                                                                        MapFileExtensionToGodotAudioStreamClass(
                                                                            audioFilePath.FileExtension)))))))
                                            .WithArgumentList(
                                                ArgumentList(
                                                    SingletonSeparatedList<ArgumentSyntax>(
                                                        Argument(
                                                            LiteralExpression(
                                                                SyntaxKind.StringLiteralExpression,
                                                                Literal(
                                                                    $"res://{audioFilePath.RelativeFilePath}")))))))))))
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

    public string AudioFolderPath = string.Empty;
    public string GodotProjectFilePath = string.Empty;
    public string? GodotProjectDirectoryPath = string.Empty;
    public ClassDeclarationSyntax? ClassDeclaration;

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        var dir = Path.Join(Path.GetDirectoryName(syntaxNode.SyntaxTree.GetLineSpan(syntaxNode.Span).Path),
            "project.godot");
        if (File.Exists(dir) && AudioFolderPath == Empty)
        {
            GodotProjectFilePath = dir;
            GodotProjectDirectoryPath = Path.GetDirectoryName(syntaxNode.SyntaxTree.GetLineSpan(syntaxNode.Span).Path);
            AudioFolderPath = Path.Join(GodotProjectDirectoryPath, "Sounds");
        }

        if (syntaxNode is AttributeSyntax { Name: IdentifierNameSyntax { Identifier.Text: "GenerateAudioPaths" } } clss)
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