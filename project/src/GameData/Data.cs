using System.Collections.Immutable;
using System.IO;
using ProtoBuf;
using ProtoBuf.Meta;
using SCALE.Models;

namespace SCALE.GameData;

[ProtoContract]
public partial class Data
{
    private const string FileName = "data.v1.bin";

    [ProtoMember(1, IsRequired = true)]
    public Settings Settings { get; } = new Settings();

    [ProtoMember(2, IsRequired = true)]

    private bool _isFirstInstructionsShown = false;
    
    [ProtoMember(3)]
    public bool IsFirstInstructionsShown
    {
        get => _isFirstInstructionsShown;
        set
        {
            _isFirstInstructionsShown = value;
            Root.EventBus.EmitOnDataPropertyChanged(nameof(IsFirstInstructionsShown));
        }
    }
    
    public static int RegisterMappings()
    {
        RuntimeTypeModel.Default.Add(typeof(Vector2), false)
            .Add(
                nameof(Vector2.X),
                nameof(Vector2.Y)
            );
        RuntimeTypeModel.Default.Add(typeof(Transform2D), false)
            .Add(
                nameof(Transform2D.X),
                nameof(Transform2D.Y),
                nameof(Transform2D.Origin)
            );
        RuntimeTypeModel.Default.Add(typeof(Color), false)
            .Add(
                nameof(Color.R),
                nameof(Color.G),
                nameof(Color.B),
                nameof(Color.A)
            );

        return 0;
    }

    public static void Save()
    {
        var path = FileUtilities.GetSavePath(FileName);
        using var stream = File.Create(path);
        // clear the file if it already exists
        stream.SetLength(0);
        Serializer.Serialize(stream, Root.Data);
        stream.Flush();
        GD.Print($"{DateTime.Now} : Saved game data to {path}");
    }

    public static Data Load()
    {
        var path = FileUtilities.GetSavePath(FileName);
        if (File.Exists(path) is false) return new Data();

        using var stream = File.OpenRead(path);
        var data = Serializer.Deserialize<Data>(stream);
        GD.Print($"{DateTime.Now} : Loaded game data from {path}");
        return data!;
    }
}