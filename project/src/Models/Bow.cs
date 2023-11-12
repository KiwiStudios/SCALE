using SCALE.Enums;

namespace SCALE.Models;

public partial class Bow : Weapon
{
    public EBowElement Element;
    public Bow(EItemNames name, int value, ERank rank, EItemLevel itemLevel) : base(name, value, rank, itemLevel)
    {
        Element = Extensions.GetRandomEnumValue<EBowElement>();
    }

    public override string DisplayName()
    {
        return $"{ItemLevel} Bow of {Element}";
    }
}