using SCALE.Enums;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Buttons;

public partial class HelpButton : ButtonPressedMove
{
    private new EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        _eventBus.EmitOnPopupOpen(EPopupNames.InitialInstructions);
    }
}