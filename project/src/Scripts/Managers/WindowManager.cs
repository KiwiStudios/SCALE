using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Managers;

public partial class WindowManager : Node
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        _eventBus.OnDataPropertyChanged += OnDataPropertyChanged;
    }

    private void OnDataPropertyChanged(string propertyname)
    {
        switch (propertyname)
        {
            case nameof(Root.Data.Settings.IsFullScreen):
                HandleIsFullScreen();
                break;
            case nameof(Root.Data.Settings.ResolutionSize):
                HandleResolutionSize();
                break;
        }
    }

    private void HandleResolutionSize()
    {
        var size = Root.Data.Settings.ResolutionSize;
        GetTree().Root.ContentScaleSize = new Vector2I(size.Width, size.Height);
    }

    private void HandleIsFullScreen()
    {
        if (Root.Data.Settings.IsFullScreen)
        {
            SetFullScreen();
        }
        else
        {
            SetWindowed();
        }
    }

    private static void SetWindowed()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Windowed);
    }

    private static void SetFullScreen()
    {
        DisplayServer.WindowSetMode(DisplayServer.WindowMode.Fullscreen);
    }
}