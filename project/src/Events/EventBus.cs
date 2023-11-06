// ReSharper disable RedundantNameQualifier

using SCALE.GameData;

namespace SCALE.Events;

public partial class EventBus : Node
{
    public static readonly NodePath NodePath = "/root/Root/EventBus";

    #region Popup management

    /// <param name="popupNames">Use the EPopupNames enum</param>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnPopupOpenEventHandler(string popupNames);

    /// <param name="popupNames">Use the EPopupNames enum</param>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnPopupCloseEventHandler(string popupNames);
    
    #endregion

    #region GlobalGameState management

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnDataPropertyChangedEventHandler(string propertyName);

    #endregion

    #region Audio management

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnRequestToPlaySoundEventHandler(AudioStream soundName);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnFinishPlayingSoundEventHandler(AudioStream soundName);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnRequestToStopSoundEventHandler(AudioStream soundName);

    #endregion

    #region Scene management

    /// <summary>
    /// Goes to specified gameState
    /// </summary>
    /// <param name="gameState">Use the EGameState enum</param>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToGameStateEventHandler(string gameState, params GodotObject[] args);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToSceneEventHandler(PackedScene scene);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToPreviousSceneEventHandler();
    
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToGameStateFinishedEventHandler(string gameState, params GodotObject[] args);

    #endregion
    
    /// <summary>
    /// Time in ticks (DateTime)
    /// </summary>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnTimeTickEventHandler(long timestamp);
}