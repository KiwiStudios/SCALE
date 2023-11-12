using SCALE.Enums;

namespace SCALE.Models;

public partial class TwoHanded : Weapon
{
    public ETwoHanded Type;
    public TwoHanded(EItemNames name, int value,
             ERank rank,
             EItemLevel itemLevel) : base(name, value, rank, itemLevel)
    {
        Type = Extensions.GetRandomEnumValue<ETwoHanded>();
    }
}