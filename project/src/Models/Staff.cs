using SCALE.Enums;

namespace SCALE.Models;

public partial class Staff : Weapon
{
    public Staff(EItemNames name, int value) : base(name, value)
    {
    }
}