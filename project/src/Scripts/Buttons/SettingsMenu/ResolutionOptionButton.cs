using System.Collections.Generic;
using EventBus = SCALE.Events.EventBus;
using ResolutionSize = SCALE.Models.ResolutionSize;

namespace SCALE.Scripts.Buttons.SettingsMenu;

public partial class ResolutionOptionButton : OptionButton
{
    private readonly List<ResolutionSize> _resolutions = new List<ResolutionSize>
    {
        new ResolutionSize(1280, 720, "1280 x 720"),
        new ResolutionSize(1920, 1080, "1920 x 1080"),
        new ResolutionSize(2560, 1440, "2560 x 1440"),
        new ResolutionSize(3840, 2160, "3840 x 2160")
    };

    private EventBus? _eventBus = null!;

    public override void _Ready()
    {
        base._Ready();
        for (var i = 0; i < _resolutions.Count; i++)
        {
            AddItem(_resolutions[i].Label, i);
        }

        Selected = _resolutions.FindIndex(resolution =>
            resolution.Width == Root.Data.Settings.ResolutionSize.Width &&
            resolution.Height == Root.Data.Settings.ResolutionSize.Height
        );
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();

        ItemSelected += OnItemSelected;
    }

    private void OnItemSelected(long index)
    {
        var idx = (int)index;
        var resolutionSize = _resolutions[idx];
        Root.Data.Settings.ResolutionSize = resolutionSize;
    }
}