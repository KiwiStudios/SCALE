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
    private bool isInitial = true;
    private bool isNewMorning = false;

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
        isNewMorning = true;
        var label = Root.SceneTree.GetFirstNodeInGroup("daystart_title") as Label ?? throw new ArgumentNullException();
        label.Text = "The travelling merchant offers wares..";
        var rowContainer = Root.SceneTree.GetFirstNodeInGroup("row_container") as VBoxContainer ?? throw new ArgumentNullException();

        _eventBus.EmitOnGoldCountChanged(Store.Gold);
        dayStartSelectedItems = new List<Item>();

        var wares = new List<Item>();
        wares.AddRange(Items.RandomBronzeRankItems(GD.RandRange(1, 3)));
        wares.AddRange(Items.RandomSilverRankItems(GD.RandRange(0, 2)));
        wares.AddRange(Items.RandomGoldRankItems(GD.RandRange(0, 1)));
        wares.AddRange(Items.RandomDiamondRankItems(GD.RandRange(0, 1)));
        wares.AddRange(Items.RandomLegendaryRankItems(GD.RandRange(0, 1)));

        AddItemsToDayStart(rowContainer, wares, newMorning: true);
        
        _eventBus.EmitOnDayStartGoldTotalChanged(dayStartSelectedItems.Sum(x => x.Value), Store.Gold);
    }

    private void OnGoToSceneFinished(PackedScene scene)
    {
        if (scene == Scenes.UI_DAYSTART_SCENE)
        {
            SetupItemSelect();
            isInitial = false;
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

        if (isInitial)
        {
            AddItemsToDayStart(rowContainer, Storage.InStorage);
        }
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
        if (isNewMorning)
        {
            Store.Gold -= dayStartSelectedItems.Sum(x => x.Value);
        }
        _eventBus.EmitOnGoToScene(Scenes.UI_SHOP_SCENE);
    }

    private void OnDayStartItemUnSelected(Item item)
    {
        dayStartSelectedItems.Remove(item);
        if (isNewMorning is true)
        {
            _eventBus.EmitOnDayStartGoldTotalChanged(dayStartSelectedItems.Sum(x => x.Value), Store.Gold);
        }
    }

    private void OnDayStartItemSelected(Item item)
    {
        dayStartSelectedItems.Add(item);
        if (isNewMorning is true)
        {
            _eventBus.EmitOnDayStartGoldTotalChanged(dayStartSelectedItems.Sum(x => x.Value), Store.Gold);
        }
    }

    private void AddItemsToDayStart(VBoxContainer rowContainer,
                                    List<Item> items,
                                    int itemsPerRow = 4,
                                    bool newMorning = false)
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

                itemContainer.SetText(item.DisplayName());

                if (newMorning)
                {
                    itemContainer.SetGoldText($"{item.Value} gold");
                }

                row.AddChild(itemContainer);
            }

            rowContainer.AddChild(row);
        }
    }

    #endregion
}