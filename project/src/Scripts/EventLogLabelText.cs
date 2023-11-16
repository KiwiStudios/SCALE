using System.Net.Mime;

namespace SCALE.Scripts;

public partial class EventLogLabelText : RichTextLabel
{
    public string BackingText = null!;
    public bool isFromNewLine = false;

    public override void _EnterTree()
    {
        Text = string.Empty;
    }
}