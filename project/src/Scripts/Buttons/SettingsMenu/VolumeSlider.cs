namespace SCALE.Scripts.Buttons.SettingsMenu;

public partial class VolumeSlider : HSlider
{
    public override void _Ready()
    {
        Value = Root.Data.Settings.Volume;
        DragEnded += OnDragEnded;
    }

    // Only save once drag is ended, otherwise we get crazy saving issues
    private void OnDragEnded(bool valueChanged)
    {
        Root.Data.Settings.Volume = (int)Value;
    }

    public override void _ValueChanged(double volume)
    {
        base._ValueChanged(volume);
        Value = volume;
    }
}