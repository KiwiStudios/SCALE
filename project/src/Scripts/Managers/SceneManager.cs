using System.Collections.Generic;
using System.Linq;

namespace SCALE.Scripts.Managers;

public partial class SceneManager : Node
{
    private static readonly Stack<PackedScene> ScenesStack = new Stack<PackedScene>();
    private static EventBus eventBus = null!;

    public override void _EnterTree()
    {
        base._Ready();
        eventBus = this.GetEventBus();
        eventBus!.OnGoToScene += SwitchScene;
        eventBus.OnGoToPreviousScene += GoBackToPreviousScene;
    }

    private static void SwitchScene(PackedScene newScene)
    {
        ScenesStack.Push(newScene);
        var instance = newScene.Instantiate<Node>();
        SetNameBasedOffFilePath(newScene, instance);
        NukeOldScenes();
        GameStateManager.Instance.AddChild(instance, true);
        eventBus.EmitOnGoToSceneFinished(newScene);
    }

    private static void NukeOldScenes()
    {
        var children = GameStateManager.Instance.GetChildren()
            .Where(x => x.Name.ToString()!.Contains("Scene"));
        foreach (var child in children)
        {
            child.QueueFree();
        }
    }

    public void CleanStack()
    {
        NukeOldScenes();
        ScenesStack.Clear();
    }

    private static void SetNameBasedOffFilePath(PackedScene newScene, Node instance)
    {
        var sceneFileString = newScene.ResourcePath.Split("/")[^1];
        var sceneString = sceneFileString.Replace(".tscn", "");
        instance.Name = $"{sceneString} Scene";
    }

    private static void GoBackToPreviousScene()
    {
        if (ScenesStack.Count > 1)
        {
            //Remove the current state from the stack
            ScenesStack.Pop();
            var prevScene = ScenesStack.Peek();
            SwitchScene(prevScene);
        }
    }
}