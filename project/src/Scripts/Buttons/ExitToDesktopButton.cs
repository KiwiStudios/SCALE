namespace SCALE.Scripts.Buttons;

public partial class ExitToDesktopButton : ButtonPressedMove
{
    public override void _Pressed()
    {
        base._Pressed();
        var tree = GetTree();
        tree.Root.PropagateNotification((int)NotificationWMCloseRequest);
        tree.Quit();
    }
}