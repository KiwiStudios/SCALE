using System.Net.Mime;
using SCALE.Constants;
using SCALE.Enums;

namespace SCALE.Scripts;

public partial class AdventurerText : EventLogLabelText
{
    private readonly Adventurer _adventurer;
    private bool isActive = false;
    private EventBus _eventBus = null!;

    public AdventurerText(Adventurer adventurer) : base()
    {
        _adventurer = adventurer;
    }

    public override void _EnterTree()
    {
        base._EnterTree();
        MouseEntered += OnMouseEntered;
        MouseExited += OnMouseExited;
    }

    public override void _Ready()
    {
        _eventBus = this.GetEventBus();
    }

    private void OnMouseExited()
    {
        isActive = false;
    }

    private void OnMouseEntered()
    {
        isActive = true;
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        if (isActive && Input.IsActionJustPressed(InputMappings.Left_Click))
        {
            _eventBus.EmitOnPopupOpen(EPopupNames.AdventurerPreview.ToString(), _adventurer);
        }
    }
}