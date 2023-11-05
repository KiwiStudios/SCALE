namespace SCALE.Scripts.Buttons;

public partial class ButtonPressedMove : Button
{
    public override void _EnterTree()
    {
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
    }

    private void OnButtonUp()
    {
        GlobalPosition += new Vector2(0, -5);
    }

    private void OnButtonDown()
    {
        GlobalPosition += new Vector2(0, 5);
    }
}