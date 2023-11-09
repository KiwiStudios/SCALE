using System.Collections.Generic;
using System.Linq;
using SCALE.Constants;
using SCALE.Enums;
using SCALE.Helpers;
using SCALE.Scripts.Buttons;

namespace SCALE.Scripts.Managers;

public partial class StoreManager : Node
{
    private EventBus _eventBus = null!;
    public Storage Storage = null!;
    public Store Store = null!;
    private Node _itemContainer = null!;

    private List<Item> dayStartSelectedItems = new List<Item>();

    public static string GroupName = nameof(StoreManager);

    public override void _EnterTree()
    {
        AddToGroup(GroupName);
        
        base._EnterTree();
        _eventBus = this.GetEventBus();

        _eventBus.OnDayStartItemSelected += OnDayStartItemSelected;
        _eventBus.OnDayStartItemUnSelected += OnDayStartItemUnSelected;
        _eventBus.OnGoToSceneFinished += OnGoToSceneFinished;
        _eventBus.OnEndDay += NewMorning;
        InitializeStore();
    }
    private void NewMorning()
    {
        Storage.InStorage.AddRange(Items.RandomItems(5));
        var rowContainer = Root.SceneTree.GetFirstNodeInGroup("row_container") as VBoxContainer ?? throw new ArgumentNullException();
        AddItemsToDayStart(rowContainer, Storage.InStorage);
    }

    private void OnGoToSceneFinished(PackedScene scene)
    {
        if (scene == Scenes.UI_DAYSTART_SCENE)
        {
            SetupItemSelect();
        }
        if (scene == Scenes.UI_SHOP_SCENE)
        {
            _eventBus.EmitOnGoldCountChanged(Store.Gold);
            _eventBus.EmitOnStartNewDay();
        }
    }

    private void SetupItemSelect()
    {
        _itemContainer = Root.SceneTree.GetFirstNodeInGroup("item_container") ?? throw new ArgumentNullException();
        var rowContainer = Root.SceneTree.GetFirstNodeInGroup("row_container") as VBoxContainer ?? throw new ArgumentNullException();
        var continueButton = Root.SceneTree.GetFirstNodeInGroup("continue_button") as ContinueButton ?? throw new ArgumentNullException();
        continueButton.Pressed += NewDay;

        AddItemsToDayStart(rowContainer, Storage.InStorage);
    }


    #region OnDayStart

    private void InitializeStore()
    {
        Storage = new Storage();
        Store = new Store();
    }

    private void NewDay()
    {
        foreach (var item in dayStartSelectedItems)
        {
            Storage.InStorage.Remove(item);
        }
        Store.items = dayStartSelectedItems;
        _eventBus.EmitOnGoToScene(Scenes.UI_SHOP_SCENE);
    }

    private void OnDayStartItemUnSelected(Item item)
    {
        dayStartSelectedItems.Remove(item);
    }

    private void OnDayStartItemSelected(Item item)
    {
        dayStartSelectedItems.Add(item);
    }

    private void AddItemsToDayStart(VBoxContainer rowContainer,
                                    List<Item> items,
                                    int itemsPerRow = 4)
    {
        for (var i = 0; i < items.Count; i += itemsPerRow)
        {
            var row = Scenes.UI_ITEM_ROW_SCENE.Instantiate();

            foreach (var item in items
                         .Skip(i)
                         .Take(itemsPerRow))
            {
                var itemContainer = Scenes.UI_ITEM_CONTAINER_SCENE.Instantiate() as ItemContainer ?? throw new ArgumentNullException();
                itemContainer.Item = item;

                itemContainer.SetText(item.Name.ToString());

                row.AddChild(itemContainer);
            }

            rowContainer.AddChild(row);
        }
    }

    #endregion
}