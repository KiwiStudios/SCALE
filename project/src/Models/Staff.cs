using SCALE.Enums;

namespace SCALE.Models;

public partial class Staff : Weapon
{
    public EStaffElement Element;
    public Staff(EItemNames name, int value, ERank rank, EItemLevel itemLevel) : base(name, value, rank, itemLevel)
    {
        Element = Extensions.GetRandomEnumValue<EStaffElement>();
    }

    public override string DisplayName()
    {
        return ItemLevel + " Staff of" + Element;
    }
}