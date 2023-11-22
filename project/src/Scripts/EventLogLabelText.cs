namespace SCALE.Scripts;

public partial class EventLogLabelText : RichTextLabel
{
    public string BackingText = null!;
    public bool AnimateText = true;

    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        if (AnimateText)
        {
            Text = string.Empty;
        }
    }
}