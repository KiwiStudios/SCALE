using System.Net.Mime;

namespace SCALE.Scripts;

public partial class EventLogLabelText : RichTextLabel
{
    public string BackingText = null!;

    public EventLogLabelText()
    {
        
    }
    
    public override void _EnterTree()
    {
        Text = string.Empty;
    }

}