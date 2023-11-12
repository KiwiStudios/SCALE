using SCALE.Enums;

namespace SCALE.Models;

public partial class Shield : Weapon
{
    public EShieldType Type;
    public Shield(EItemNames name, int value,
             ERank rank,
             EItemLevel itemLevel) : base(name, value, rank, itemLevel)
    {
        Type = Extensions.GetRandomEnumValue<EShieldType>();
    }
}