using SCALE.Constants;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Buttons;

public partial class PlayButton : ButtonPressedMove
{
    private EventBus? _eventBus = default!;

    public override void _Ready()
    {
        base._Ready();
        _eventBus = this.GetEventBus();
    }

    public override void _Pressed()
    {
       
    }
}