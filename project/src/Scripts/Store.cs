using System.Collections.Generic;

namespace SCALE.Scripts;

public class Store
{
    public List<Item> items = new List<Item>();
    public int Gold = 20;

    public bool BuyItem(EventBus eventBus,
                        Item item,
                        Adventurer adventurer)
    {
        var profitPercentage = GD.Randf() + 1;
        var gains = (int)(item.Value * profitPercentage);
        Gold += gains;
        items.Remove(item);
        eventBus.EmitOnGoldCountChanged(Gold);
        eventBus.EmitOnItemSold(item, adventurer);
        return true;
    }
}