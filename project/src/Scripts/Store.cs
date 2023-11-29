using System.Collections.Generic;

namespace SCALE.Scripts;

public class Store
{
    public List<Item> items = new List<Item>();
    public int Gold = 200;

    public bool BuyItem(EventBus eventBus,
                        Item item,
                        Adventurer adventurer)
    {
        var profitPercentage = GD.RandRange(0.1f, 0.5f) + 1;
        var gains = (int)(item.Value * profitPercentage);
        Gold += gains;
        items.Remove(item);
        eventBus.EmitOnGoldCountChanged(Gold);
        eventBus.EmitOnItemSold(item, adventurer);
        return true;
    }
}