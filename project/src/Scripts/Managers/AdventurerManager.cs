namespace SCALE.Scripts.Managers;

public partial class AdventurerManager : Node
{
    private EventBus _eventBus = null!;
    public override void _EnterTree()
    {
        var eventBus = this.GetEventBus();
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
        _eventBus.OnTimeTick += AdventureBuying;
        
    }
    private void AdventureBuying(long timestamp)
    {
        //throw new NotImplementedException();
    }
}