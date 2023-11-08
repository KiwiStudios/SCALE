using SCALE.Enums;

namespace SCALE.Models;

public partial class Armour : Item
{
    public Armour(EItemNames name, int value) : base(name, value)
    {
    }
}