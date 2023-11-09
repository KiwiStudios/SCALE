namespace SCALE.Scripts.Buttons;

public partial class SkipDayButton : Button
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
    }

    public override void _Pressed()
    {
        base._Pressed();
        _eventBus.EmitOnSkipRestOfDay();
    }
}