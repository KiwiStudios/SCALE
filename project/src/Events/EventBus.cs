
using EventBusSourceGenerator;
using SCALE.GameData;
using SCALE.Scripts.Managers;

namespace SCALE.Events;

public partial class EventBus : Node
{
    public static readonly NodePath NodePath = "/root/Root/EventBus";

    #region Popup management

    /// <param name="popupNames" type="EPopupNames">Use the EPopupNames enum</param>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnPopupOpenEventHandler(string popupNames, params GodotObject[] args);

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
    public delegate void OnGoToSceneFinishedEventHandler(PackedScene scene);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToPreviousSceneEventHandler();
    
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoToGameStateFinishedEventHandler(string gameState, params GodotObject[] args);

    #endregion

    #region Store

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnGoldCountChangedEventHandler(int goldCount);
    
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnDayStartItemSelectedEventHandler(Item item);
    
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnDayStartItemUnSelectedEventHandler(Item item);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnItemSoldEventHandler(Item item, Adventurer adventurer);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnDayStartGoldTotalChangedEventHandler(int goldTotalInCart, int moneyInStore);

    
    /// <summary>
    /// Use the ERank enum.
    /// </summary>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnAdventurerLevelUpEventHandler(string rankFrom, string rankTo, Adventurer adventurer);


    #endregion

    #region TimeManagment

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnStopTimeEventHandler();

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnStartTimeEventHandler();

    /// <summary>
    /// Emitted after the day ends, (before the setting up shop is loaded)
    /// </summary>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnEndDayEventHandler();

    /// <summary>
    ///  Emitted after setting up the shop and the day actually starts
    /// </summary>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnStartNewDayEventHandler();

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnSkipRestOfDayEventHandler();
    
    #endregion

    #region Adventurers

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnAdventurerDeathEventHandler(Adventurer adventurer);

    [Signal, GenerateMethodForEventHandler]
    public delegate void OnAdventurerGoesOnQuestEventHandler(Adventurer adventurer, Quest quest);

    #endregion
    
    /// <summary>
    /// Time in ticks (DateTime)
    /// </summary>
    [Signal, GenerateMethodForEventHandler]
    public delegate void OnTimeTickEventHandler(long timestamp, int minutes);
}