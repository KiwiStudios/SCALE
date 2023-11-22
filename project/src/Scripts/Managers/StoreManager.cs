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
    private AdventurerManager _adventurerManager = null!;

    public override void _EnterTree()
    {
        AddToGroup(GroupName);

        base._EnterTree();
        _eventBus = this.GetEventBus();

        _adventurerManager = (AdventurerManager)Root.SceneTree.GetFirstNodeInGroup(AdventurerManager.GroupName);
        _eventBus.OnDayStartItemSelected += OnDayStartItemSelected;
        _eventBus.OnDayStartItemUnSelected += OnDayStartItemUnSelected;
        _eventBus.OnGoToSceneFinished += OnGoToSceneFinished;
        _eventBus.OnEndDay += NewMorning;
        InitializeStore();
    }

    public List<float> HistoryOfAdventurerScores = new List<float>();
    public float CurrentItemScalingFactor = 1f;

    private void NewMorning()
    {
        isNewMorning = true;
        var label = Root.SceneTree.GetFirstNodeInGroup("daystart_title") as Label ?? throw new ArgumentNullException();
        label.Text = "The travelling merchant offers wares..";
        var rowContainer = Root.SceneTree.GetFirstNodeInGroup("row_container") as VBoxContainer ?? throw new ArgumentNullException();

        var adventurerScores = _adventurerManager.Adventurers.Sum(x => x.GetTotalAdventurerScore());
        HistoryOfAdventurerScores.Add(adventurerScores);

        if (HistoryOfAdventurerScores.Count > 1)
        {
            var diff = HistoryOfAdventurerScores[^1] - HistoryOfAdventurerScores[0];
            var percentageChange = diff / Math.Abs(HistoryOfAdventurerScores[0]);
            CurrentItemScalingFactor = 1f + percentageChange;
        }

        _eventBus.EmitOnGoldCountChanged(Store.Gold);
        dayStartSelectedItems = new List<Item>();

        var wares = new List<Item>();

        var bronzeMax = (int)(2 * CurrentItemScalingFactor * 4.5f);
        var silverMax = (int)(2 * CurrentItemScalingFactor * 4);
        var goldMax = (int)(1 * CurrentItemScalingFactor * 3);
        var diamondMax = (int)(1 * CurrentItemScalingFactor * 2);
        var legendaryMax = (int)(1 * CurrentItemScalingFactor * 1.5f);
        
        Console.WriteLine($"{nameof(bronzeMax)} : {bronzeMax}");
        Console.WriteLine($"{nameof(silverMax)} : {silverMax}");
        Console.WriteLine($"{nameof(goldMax)} : {goldMax}");
        Console.WriteLine($"{nameof(diamondMax)} : {diamondMax}");
        Console.WriteLine($"{nameof(legendaryMax)} : {legendaryMax}");
        Console.Write("");

        wares.AddRange(Items.RandomBronzeRankItems(GD.RandRange(1, bronzeMax)));
        wares.AddRange(Items.RandomSilverRankItems(GD.RandRange(0, silverMax)));
        wares.AddRange(Items.RandomGoldRankItems(GD.RandRange(0, goldMax)));
        wares.AddRange(Items.RandomDiamondRankItems(GD.RandRange(0, diamondMax)));
        wares.AddRange(Items.RandomLegendaryRankItems(GD.RandRange(0, legendaryMax)));


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