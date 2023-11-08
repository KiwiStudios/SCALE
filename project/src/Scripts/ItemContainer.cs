using Godot;
using System;
using System.Reflection;
using SCALE;
using SCALE.Scripts.Buttons;

public partial class ItemContainer : ButtonPressedMove
{
    public Item Item { get; set; } = null!;

    private bool _selected = false;

    public override void _EnterTree()
    {
        PlaySound = false;
        _eventBus = this.GetEventBus();
    }

    public override void _Pressed()
    {
        base._Pressed();

        _selected = !_selected;
        
        if (_selected)
        {
            _eventBus.EmitOnDayStartItemSelected(Item);
            Select();
        }
        else
        {
            _eventBus.EmitOnDayStartItemUnSelected(Item);
            Unselect();
        }
        
    }

    public void Select()
    {
        var modulate = Modulate;
        modulate = Colors.ForestGreen;
        Modulate = modulate;
    }

    public void Unselect()
    {
        var modulate = Modulate;
        modulate = Colors.White;
        Modulate = modulate;
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        if (IsHovered())
        {
            var modulate = Modulate;
            modulate.A = 0.8f;
            Modulate = modulate;
        }
        else
        {
            var modulate = Modulate;
            modulate.A = 1f;
            Modulate = modulate;
        }
    }

    public void SetText(string text)
    {
        var label = GetNode<Label>("Panel/MarginContainer/VBoxContainer2/ItemText/Label");
        label.Text = text;
    }
}