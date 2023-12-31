using SCALE.Constants;

namespace SCALE.Scripts.Buttons;

public partial class ButtonPressedMove : Button
{
    private bool isHovered = false;
    private bool playedSound = false;
    public EventBus _eventBus = null!;
    public bool PlaySound = true;

    public override void _EnterTree()
    {
        ButtonDown += OnButtonDown;
        ButtonUp += OnButtonUp;
        _eventBus = this.GetEventBus();
    }

    public override void _Process(double delta)
    {
        if (!PlaySound) return;
        
        if (isHovered != IsHovered())
        {
            isHovered = IsHovered();

            if (isHovered && !playedSound)
            {
                _eventBus.EmitOnRequestToPlaySound(AudioPaths.UI_SOUNDPACK_WAV_MODERN7_WAV);
                playedSound = true;
            }
        }

        if (isHovered == false)
        {
            playedSound = false;
        }
    }

    private void OnButtonUp()
    {
        GlobalPosition += new Vector2(0, -5);
    }

    private void OnButtonDown()
    {
        GlobalPosition += new Vector2(0, 5);

        if (!PlaySound) return;
        
        _eventBus.EmitOnRequestToPlaySound(AudioPaths.UI_SOUNDPACK_WAV_MODERN8_WAV);
    }
}