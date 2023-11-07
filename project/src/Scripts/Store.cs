using System.Collections.Generic;

namespace SCALE.Scripts;

public class Store
{
    public List<Item> items = new List<Item>();
    public int Gold = 20;

    public void BuyItem(EventBus eventBus, Item item)
    {
        Gold += item.Value;
        items.Remove(item);
        eventBus.EmitOnGoldCountChanged(Gold);
    }
}