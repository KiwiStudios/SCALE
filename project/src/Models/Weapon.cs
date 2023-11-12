using System.Collections.Generic;
using SCALE.Enums;

namespace SCALE.Models;

public partial class Weapon : Item
{
    public ERank Rank;
    public EItemLevel ItemLevel;
    public Weapon(EItemNames name, int value, ERank rank, EItemLevel itemLevel) : base(name, value)
    {
        Rank = rank;
        ItemLevel = itemLevel;
    }
}