namespace SCALE.Constants;

/// <summary>
/// Will generate string constants from Sounds folder where project.godot is the root it looks for the folder
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public sealed class GenerateAudioPathsAttribute : Attribute
{
}