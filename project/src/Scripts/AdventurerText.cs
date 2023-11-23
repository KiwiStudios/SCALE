using System.Net.Mime;
using SCALE.Constants;
using SCALE.Enums;

namespace SCALE.Scripts;

public partial class AdventurerText : EventLogLabelText
{
    public Adventurer Adventurer = null!;
    private bool isActive = false;
    private EventBus _eventBus = null!;

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
            _eventBus.EmitOnPopupOpen(EPopupNames.AdventurerPreview, Adventurer);
        }
    }
}