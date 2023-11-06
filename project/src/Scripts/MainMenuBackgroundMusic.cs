namespace SCALE.Scripts;

public partial class MainMenuBackgroundMusic : AudioStreamPlayer
{
	public override void _Ready()
	{
		PlaySound();

		this.Finished += OnFinished;
	}

	private void PlaySound()
	{
		var range = Stream.GetLength() * 0.1f;

		var rnd = GD.RandRange(range, Stream.GetLength() - range);
		Play((float)rnd);
	}

	private void OnFinished()
	{
		PlaySound();
	}

	public override void _Process(double delta)
	{
	}
}
