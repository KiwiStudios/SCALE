namespace SCALE.Constants;

[GenerateScenePaths]
public partial class Scenes
{
    public static PackedScene GetSceneForFilePath(string filePath)
    {
        return ResourceLoader.Load<PackedScene>(filePath);
    } 
}