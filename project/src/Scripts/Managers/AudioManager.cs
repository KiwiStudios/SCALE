namespace SCALE.Scripts.Managers;

public partial class AudioManager : Node
{
    private static AudioManager? _instance;
    public static AudioManager Instance
    {
        get { return _instance ??= new AudioManager(); }
    }

    public override void _EnterTree()
    {
        var eventBus = this.GetEventBus();

        eventBus.OnRequestToPlaySound += EventBusOnOnRequestToPlaySound;
        eventBus.OnRequestToStopSound += EventBusOnOnRequestToStopSound;
    }

    private void EventBusOnOnRequestToStopSound(AudioStream soundname)
    {
        var children = GetChildren();
        foreach (var child in children)
        {
            if (child is AudioStreamPlayer asp)
            {
                if (asp.Stream.ResourcePath == soundname.ResourcePath)
                {
                    child.QueueFree();
                }
            }
        }
    }

    private void EventBusOnOnRequestToPlaySound(AudioStream soundname)
    {
        var eventBus = this.GetEventBus();
        var asp = new AudioStreamPlayer()
        {
            Stream = soundname,
            Autoplay = true,
            VolumeDb = Root.Data.Settings.Volume
        };

        asp.Finished += () =>
        {
            eventBus!.EmitOnFinishPlayingSound(soundname);
            asp.QueueFree();
        };

        AddChild(asp);
    }
}