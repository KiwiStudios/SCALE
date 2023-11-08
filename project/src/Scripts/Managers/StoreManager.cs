using System.Collections.Generic;
using System.Linq;
using SCALE.Constants;
using SCALE.Enums;

namespace SCALE.Scripts.Managers;

public partial class StoreManager : Node
{
    private EventBus _eventBus = null!;
    public Storage Storage = null!;
    public Store Store = null!;
    private Node _itemContainer = null!;

    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();

        _eventBus.OnGoToGameStateFinished += OnGoToGameStateFinished;
    }

    private void OnGoToGameStateFinished(string gamestate, GodotObject[] args)
    {
        if (gamestate == EGameState.Playing.ToString())
        {
            InitializeStore();
        }
    }

    private void InitializeStore()
    {
        base._Ready();
        Storage = new Storage();
        Store = new Store();

        //This is just for testing should still be reworked
        Store.items = Storage.InStorage;

        _itemContainer = Root.SceneTree.GetFirstNodeInGroup("item_container") ?? throw new ArgumentNullException();
        var rowContainer = Root.SceneTree.GetFirstNodeInGroup("row_container") as VBoxContainer ?? throw new ArgumentNullException();

        _eventBus.OnDayStartItemSelected += OnDayStartItemSelected;
        _eventBus.OnDayStartItemUnSelected += OnDayStartItemUnSelected;
        
        AddItemsToDayStart(rowContainer, Storage.InStorage);

        _eventBus.EmitOnGoldCountChanged(Store.Gold);
    }

    private void OnDayStartItemUnSelected(Item item)
    {
        
    }

    private void OnDayStartItemSelected(Item item)
    {
        
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
}