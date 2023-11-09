using SCALE.Scripts.Managers;

namespace SCALE.Scripts;

public partial class DayDisplay : Label
{
    private EventBus _eventBus = null!;
    private TimeManager _timeManager = null!;

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
        _eventBus.OnStartNewDay += OnNewDay;
    }

    public override void _Ready()
    {
        base._Ready();
        _timeManager = (TimeManager)Root.SceneTree.GetFirstNodeInGroup(TimeManager.GroupName);
    }

    private void OnNewDay()
    {
        Text = "Day " + _timeManager.Day;
    }
    
}