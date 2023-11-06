using SCALE.Enums;

namespace SCALE.Models;

public class Item
{
    public Item(EItemNames name, int value)
    {
        Name = name;
        Value = value;
    }
    
    public EItemNames Name { get; set; }
    public int Value { get; set; }
}