using SCALE.Constants;

namespace SCALE.Scripts.GameStates;

public partial class MainMenuState : GameState
{
    public override void _EnterTree()
    {
        base._EnterTree();
        var eventBus = this.GetEventBus();
        eventBus!.EmitOnGoToScene(Scenes.GAMESTATES_MAIN_SCENE);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(InputMappings.Escape_Key))
        {
            var eventBus = this.GetEventBus();
            eventBus!.EmitOnGoToPreviousScene();
        }
    }
}