using System.Collections.Generic;

namespace SCALE.Scripts.Managers;

public partial class AdventurerManager : Node
{
    private EventBus _eventBus = null!;
    private static int adventureCount = 50;

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
        
    }

    private static List<Adventurer> AdventurerList()
    {
        var t = new List<Adventurer>();
        for (int i = 0; i < adventureCount; i++)
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
        var buyChance = 5;
        var willBuy =  GD.RandRange(0, 100) < buyChance;
        if (willBuy)
        {
            BuyRandomItem(adventurer);
        }
    }

    private void BuyRandomItem(Adventurer adventurer)
    {
        
    }
}