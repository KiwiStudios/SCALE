namespace SCALE.Scripts;

public partial class EventLogLabelText : RichTextLabel
{
    public string BackingText = null!;

    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        Text = string.Empty;
    }
}