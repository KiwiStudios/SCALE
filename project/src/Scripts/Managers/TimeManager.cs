using SCALE.Constants;

namespace SCALE.Scripts.Managers;

public partial class TimeManager : Node
{
    private double deltaSum = 0;

    /// <summary>
    /// Every Threshold amount we signify a 'tick'
    /// </summary>
    public static float Threshold = 0.2f; //0.025f;

    private EventBus _eventBus = null!;
    public static bool IsTimeTicking;
    public static int TickTime = 5;
    public static DateTime StartOfDayTime = DateTime.Today.AddHours(6);
    public static DateTime CurrentTime = StartOfDayTime;
    public static DateTime NightTime = DateTime.Today.AddHours(18);
    public int Day = 1;
    public static string GroupName = nameof(TimeManager);

    public override void _EnterTree()
    {
        base._EnterTree();
        AddToGroup(GroupName);
        _eventBus = this.GetEventBus();
        _eventBus.OnStopTime += StopTime;
        _eventBus.OnStartTime += StartTime;
        _eventBus.OnSkipRestOfDay += OnSkipRestOfDay;
        _eventBus.OnStartNewDay += StartTime;
    }
    private void OnSkipRestOfDay()
    {
        EndDay();
    }
    private void StartTime()
    {
        IsTimeTicking = true;
    }
    private void StopTime()
    {
        IsTimeTicking = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(!IsTimeTicking) return;
        
        deltaSum += delta;
        
        if (!(deltaSum > Threshold)) return;
        deltaSum = 0;
        CurrentTime = CurrentTime.AddMinutes(TickTime);
        _eventBus.EmitOnTimeTick(CurrentTime.Ticks, TickTime);

        if (CurrentTime.Hour < NightTime.Hour) return;
        EndDay();
    }

    public void EndDay()
    {
        Day++;
        IsTimeTicking = false;
        CurrentTime = StartOfDayTime;
        _eventBus.EmitOnStopTime();
        _eventBus.EmitOnGoToScene(Scenes.UI_DAYSTART_SCENE);
        _eventBus.EmitOnEndDay();
    }
}