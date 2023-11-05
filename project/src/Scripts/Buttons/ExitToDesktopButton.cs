namespace SCALE.Scripts.Buttons;

public partial class ExitToDesktopButton : ButtonPressedMove
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public override void _Pressed()
    {
        var tree = GetTree();
        tree.Root.PropagateNotification((int)NotificationWMCloseRequest);
        tree.Quit();
    }
}