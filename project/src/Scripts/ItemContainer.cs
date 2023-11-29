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
		_eventBus.OnDayStartGoldTotalChanged += OnDayStartGoldTotalChanged;
		
	}

	private void OnDayStartGoldTotalChanged(int goldTotalInCart, int moneyInStore)
	{
		if (goldTotalInCart + Item.Value > moneyInStore && !_selected)
		{
			Disabled = true;
			Disable();
		}
		else if (Disabled)
		{
			Disabled = false;
			Unselect();
		}
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

	public void Disable()
	{
		var modulate = Modulate;
		modulate = Colors.Gray;
		modulate.A = 0.2f;
		Modulate = modulate;
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

	public void SetGoldText(string text)
	{
		var label = GetNode<Label>("Panel/MarginContainer/VBoxContainer2/ItemText/GoldCost");
		label.Text = text;
	}
	
	public void SetText(string text)
	{
		var label = GetNode<Label>("Panel/MarginContainer/VBoxContainer2/ItemText/ItemName");
		label.Text = text;
	}
}
