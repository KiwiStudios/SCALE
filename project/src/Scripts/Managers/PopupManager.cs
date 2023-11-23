using SCALE.Constants;
using SCALE.Enums;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.Managers;

public partial class PopupManager : Node
{
    public EPopupNames? currentPopup;
    private EventBus _eventbus = null!;

    public override void _EnterTree()
    {
        _eventbus = this.GetEventBus();

        _eventbus.OnPopupOpen += OnPopupOpen;
        _eventbus.OnPopupClose += OnPopupClose;
    }

    private void OnPopupOpen(EPopupNames popupName, params GodotObject[] args)
    {
        if (currentPopup is not null) return;
        var popup = popupName switch
        {
            EPopupNames.InitialInstructions => Scenes.UI_TUTORIAL_POPUP_SCENE.Instantiate<Popup>(),
            EPopupNames.AdventurerPreview => Scenes.UI_ADVENTURER_POPUP_SCENE.Instantiate<AdventurerPopup>(),
            _ => throw new ArgumentOutOfRangeException()
        };

        if (popup is AdventurerPopup adventurerPopup)
        {
            adventurerPopup.Adventurer = (Adventurer)args[0];
        }

        popup.Name = popupName.ToString();
        popup.PopupHide += PopupOnPopupHide;
        Root.Tree.AddChild(
            popup,
            true
        );
        currentPopup = popupName;
    }


    private void PopupOnPopupHide()
    {
        if (currentPopup is not null)
        {
            OnPopupClose(currentPopup.Value);
        }
    }

    private void OnPopupClose(EPopupNames popupName)
    {
        //Enum.Parse<EPopupNames>(popupName);
        if (currentPopup is null) return;
        var popup = Root.Tree.GetNodeOrNull<Popup>($"/root/{popupName}");
        popup.QueueFree();
        currentPopup = null;
    }
}