// ReSharper disable RedundantDefaultMemberInitializer

using ProtoBuf;
using SCALE.Models;

namespace SCALE.Models;

public interface IDataProperty
{
    public void Load();
}

[ProtoContract]
public partial class Settings
{
    private bool _isFullScreen = false;
    private ResolutionSize _resolutionSize = new ResolutionSize(1280, 720, "1280 x 720");
    private int _volume = 0;

    [ProtoMember(1)]
    public int Volume
    {
        get => _volume;
        set
        {
            _volume = value;
            Root.EventBus.EmitOnDataPropertyChanged(nameof(Volume));
        }
    }

    [ProtoMember(2, IsRequired = true)]
    public ResolutionSize ResolutionSize
    {
        get => _resolutionSize;
        set
        {
            _resolutionSize = value;
            Root.EventBus.EmitOnDataPropertyChanged(nameof(ResolutionSize));
        }
    }

    [ProtoMember(3)]
    public bool IsFullScreen
    {
        get => _isFullScreen;
        set
        {
            _isFullScreen = value;
            Root.EventBus.EmitOnDataPropertyChanged(nameof(IsFullScreen));
        }
    }
}