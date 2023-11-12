using SCALE.Enums;

namespace SCALE.Models;

public partial class Bow : Weapon
{
    public EBowElement Element;
    public Bow(EItemNames name, int value) : base(name, value)
    {
        Element = Extensions.GetRandomEnumValue<EBowElement>();
    }
}