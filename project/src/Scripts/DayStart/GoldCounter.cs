namespace SCALE.Scripts.DayStart;

public partial class GoldCounter : Label
{
	
	private EventBus _eventbus = null!;
	public override void _EnterTree()
	{
		base._EnterTree();
		_eventbus = this.GetEventBus();
		_eventbus.OnGoldCountChanged += UpdateText;
	}

	private void UpdateText(int goldCount)
	{
		Text = "Gold: " + goldCount;
	}
}
