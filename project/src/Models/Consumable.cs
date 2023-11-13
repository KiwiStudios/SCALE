using SCALE.Enums;

namespace SCALE.Models;

public partial class Consumable : Item
{
    public EPotionTypes Type;
    public EVialSize Size;
    public Consumable(EItemNames name, int value, EPotionTypes type, EVialSize size) : base(name, value)
    {
        Type = type;
        Size = size;
    }

    public override string DisplayName()
    {
        return $"{Size} {Type} Potion";
    }
}