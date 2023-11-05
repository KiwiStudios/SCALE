using ProtoBuf;

namespace SCALE.Models;

[ProtoContract]
public partial class ResolutionSize : GodotObject
{
    public ResolutionSize(int width,
                          int height,
                          string label)
    {
        Width = width;
        Height = height;
        Label = label;
    }

    [ProtoMember(1)]
    public int Width { get; set; }
    [ProtoMember(2)]
    public int Height { get; set; }
    [ProtoMember(3)]
    public string Label { get; set; }
}