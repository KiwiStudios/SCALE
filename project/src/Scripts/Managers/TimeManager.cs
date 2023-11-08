namespace SCALE.Scripts.Managers;

public partial class TimeManager : Node
{
    private double deltaSum = 0;

    /// <summary>
    /// Every Threshold amount we signify a 'tick'
    /// Next to that, we increase the in-game clock with a varied amount every tick
    /// </summary>
    private float Threshold = 5;

    private EventBus _eventBus = null!;
    public static bool TimeTicking;
    public static long CurrentTime = DateTime.UtcNow.Ticks;

    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();
        _eventBus.OnStopTime += StopTime;
        _eventBus.OnStartTime += StartTime;
    }
    private void StartTime()
    {
        TimeTicking = true;
    }
    private void StopTime()
    {
        TimeTicking = false;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if(!TimeTicking) return;
        
        deltaSum += delta;
        if (deltaSum > Threshold)
        {
            deltaSum = 0;

            var time = new DateTime(CurrentTime);
            CurrentTime = time.AddMinutes(GD.RandRange(2, 10)).Ticks;
            _eventBus.EmitOnTimeTick(CurrentTime);
        }
    }
}