namespace SCALE.Scripts.GameStates;

public abstract partial class GameState : Node2D
{
    // force implementation of _Input in GameStates
    public abstract override void _Input(InputEvent @event);
}