using Godot;
using System;

public partial class DayStartBackground : Sprite2D
{
// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var viewport = GetViewportRect().Size;
		Position = new Vector2(viewport.X / 2, viewport.Y / 2);
		// scale to viewport size

		var scale = new Vector2(1, 1);

		// 1280x720 is our default resolution where we scale everything from
		if (viewport.X > 512)
		{
			scale.X = viewport.X / 512;
		}

		if (viewport.Y > 512)
		{
			scale.Y = viewport.Y / 512;
		}

		Scale = scale;
		
	}
}
