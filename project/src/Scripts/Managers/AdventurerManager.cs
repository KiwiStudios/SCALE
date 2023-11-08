using System.Collections.Generic;

namespace SCALE.Scripts.Managers;

public partial class AdventurerManager : Node
{
    private EventBus _eventBus = null!;
    private const int AdventureCount = 50;
    private StoreManager _storeManager = null!;
    
    private List<Adventurer> _adventurers = AdventurerList();
    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
        _eventBus.OnTimeTick += AdventureBuying;
        _storeManager = (StoreManager)Root.SceneTree.GetFirstNodeInGroup(StoreManager.GroupName);
    }

    private static List<Adventurer> AdventurerList()
    {
        var t = new List<Adventurer>();
        for (int i = 0; i < AdventureCount; i++)
        {
            t.Add(new Adventurer());
        }
        return t;
    }
    
    private void AdventureBuying(long timestamp)
    {
        foreach (var adventurer in _adventurers)
        {
            SimulateDay(adventurer);
        }
    }

    private void SimulateDay(Adventurer adventurer)
    {
        var buyChance = 1;
        var willBuy =  GD.RandRange(0, 100) < buyChance;
        if (willBuy)
        {
            BuyRandomItem(adventurer);
        }
    }

    private void BuyRandomItem(Adventurer adventurer)
    {
        var storeItems = _storeManager.Store.items;
        if (storeItems.Count <= 0) return;
        var randItemIndex =  GD.RandRange(0, storeItems.Count - 1);
        var randItem = storeItems[randItemIndex];
        _storeManager.Store.BuyItem(_eventBus, randItem);
    }
}