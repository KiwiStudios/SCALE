using Godot;
using System;

public partial class ItemContainer : BoxContainer
{
	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
	}

    public void SetText(string text)
    {
        var label = GetNode<Label>("Panel/MarginContainer/VBoxContainer2/ItemText/Label");
        label.Text = text;
    }
}
