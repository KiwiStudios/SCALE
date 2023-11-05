namespace SCALE.Scripts;

public partial class Camera : Camera2D
{
    public override void _Ready()
    {
        Position = new Vector2(1280 / 2, 720 / 2);
    }

    public override void _Process(double delta)
    {
        var currentViewport = GetViewportRect().Size;

        var zoom = new Vector2(1, 1);

        // 1280x720 is our default resolution where we scale everything from
        if (currentViewport.X > 1280)
        {
            zoom.X = currentViewport.X / 1280;
        }

        if (currentViewport.Y > 720)
        {
            zoom.Y = currentViewport.Y / 720;
        }

        Zoom = zoom;
    }
}