using SCALE.Scripts.Managers;

namespace SCALE.Scripts;

public partial class TimeDisplay : Label
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
        _eventBus.OnTimeTick += OnTimeTick;
    }

    public override void _Ready()
    {
        base._Ready();
        Text = GetDateText(TimeManager.CurrentTime);
    }

    private void OnTimeTick(long timestamp)
    {
        Text = GetDateText(new DateTime(timestamp));
    }

    private static string GetDateText(DateTime dateTime)
    {
        return dateTime.ToString("HH:mm:ss");
    }
}