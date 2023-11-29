using SCALE.Enums;
using SCALE.Scripts.GameStates;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Managers;

public partial class GameStateManager : Node
{
    private static GameStateManager? _instance;
    private SceneManager? _sceneManager;
    public EGameState CurGameState = EGameState.MainMenu;
    private GameState? _gameStateNode;
    private EventBus? _eventBus = default!;

    public static GameStateManager Instance
    {
        get { return _instance ??= new GameStateManager(); }
    }

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        _eventBus!.OnGoToGameState += SwitchState;
    }

    public override void _Ready()
    {
        SpawnSceneManager();
        SpawnInitialGamestate();
    }

    private void SpawnInitialGamestate()
    {
        if (_gameStateNode is not null) return;

        _gameStateNode = GetGameStateNode(CurGameState);
        _gameStateNode.Name = CurGameState + "GameState";
        AddChild(_gameStateNode, true);
    }

    private void SpawnSceneManager()
    {
        if (_sceneManager is not null) return;

        _sceneManager = new SceneManager();
        _sceneManager.Name = "SceneManager";
        AddChild(_sceneManager, true);
    }

    private static GameState GetGameStateNode(EGameState gameState, GodotObject[]? args = null)
    {
        return gameState switch
        {
            EGameState.Playing => new PlayingGameState(args),
            EGameState.MainMenu => new MainMenuState(),
            _ => throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null)
        };
    }

    private void SwitchState(EGameState newGameState, GodotObject[] args)
    {
        _gameStateNode?.QueueFree();
        _sceneManager?.CleanStack();
        var newGameStateNode = GetGameStateNode(newGameState, args);
        newGameStateNode.Name = newGameState + "GameState";
        _gameStateNode = newGameStateNode;
        AddChild(newGameStateNode, true);

        _eventBus!.EmitOnGoToGameStateFinished(newGameState, args);
    }
}