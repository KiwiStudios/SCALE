namespace SCALE.Scripts;

public partial class Background : Sprite2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var viewport = GetViewportRect().Size;
		Position = new Vector2(1280 / 2, 720 / 2);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var viewport = GetViewportRect().Size;
		// bg is 1024x1024
		// scale to viewport size

		var scale = new Vector2(1, 1);

		// 1280x720 is our default resolution where we scale everything from
		if (viewport.X > 1024)
		{
			scale.X = viewport.X / 1024;
		}

		if (viewport.Y > 1024)
		{
			scale.Y = viewport.Y / 1024;
		}

		Scale = scale;
	}
}
