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

    private void OnPopupOpen(string popupName)
    {
        if (currentPopup is not null) return;
        var ePopupName = Enum.Parse<EPopupNames>(popupName);
        var popup = ePopupName switch
        {
            EPopupNames.InitialInstructions => Scenes.UI_TUTORIAL_POPUP_SCENE.Instantiate<Popup>(),
            _ =>  throw new ArgumentOutOfRangeException()
        };
        popup.Name = popupName;
        popup.PopupHide += PopupOnPopupHide;
        Root.Tree.AddChild(
            popup,
            true
        );
        currentPopup = ePopupName;
    }
    
    
    private void PopupOnPopupHide()
    {
        OnPopupClose(currentPopup.ToString()!);
    }

    private void OnPopupClose(string popupName)
    {
        Enum.Parse<EPopupNames>(popupName);
        if (currentPopup is null) return;
        var popup = Root.Tree.GetNodeOrNull<Popup>($"/root/{popupName}");
        popup.QueueFree();
        currentPopup = null;
    }
}