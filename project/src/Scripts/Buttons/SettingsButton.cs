using SCALE.Constants;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Buttons;

public partial class SettingsButton : ButtonPressedMove
{
    private EventBus? _eventBus = default!;

    public override void _Ready()
    {
        base._Ready();
        _eventBus = this.GetEventBus();
    }

    public override void _Pressed()
    {
        _eventBus!.EmitOnGoToScene(Scenes.UI_SETTINGS_SCENE);
    }
}