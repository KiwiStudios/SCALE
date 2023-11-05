using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Buttons.SettingsMenu;

public partial class FullScreenToggle : CheckButton
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        ButtonPressed = Root.Data.Settings.IsFullScreen;
    }

    public override void _Toggled(bool buttonPressed)
    {
        base._Toggled(buttonPressed);
        Root.Data.Settings.IsFullScreen = buttonPressed;
    }
}