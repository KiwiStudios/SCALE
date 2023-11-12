using SCALE.Enums;

namespace SCALE.Models;

public partial class Shield : Weapon
{
    public EShieldType Type;
    public int ArmourRating;
    public Shield(EItemNames name, int value,
             ERank rank,
             EItemLevel itemLevel) : base(name, value, rank, itemLevel)
    {
        Type = Extensions.GetRandomEnumValue<EShieldType>();
        ArmourRating = itemLevel switch
        {
            EItemLevel.Simple => 3,
            EItemLevel.Enchanted => 5,
            EItemLevel.Magical => 8,
            EItemLevel.Wondrous => 12,
            EItemLevel.Godly => 18,
            _ => throw new ArgumentOutOfRangeException(nameof(itemLevel), itemLevel, null)
        };
    }

    public override string DisplayName()
    {
        return $"{ItemLevel} {Type} Shield";
    }
}