using SCALE.Enums;

namespace SCALE.Models;

public partial class OneHanded : Weapon
{
    private EOneHanded Type;
    public OneHanded(
        EItemNames name, 
        int value,
        ERank rank,
        EItemLevel itemLevel
        ) : base(name, value, rank, itemLevel)
    {
        Type = Extensions.GetRandomEnumValue<EOneHanded>();
    }
}