using SCALE.Enums;

namespace SCALE.Models;

public partial class Staff : Weapon
{
    public EStaffElement Element;
    public Staff(EItemNames name, int value) : base(name, value)
    {
        Element = Extensions.GetRandomEnumValue<EStaffElement>();
    }
}