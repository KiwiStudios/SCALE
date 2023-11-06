using System.Collections.Generic;
using SCALE.Enums;

namespace SCALE.Models;

public class Weapon : Item
{
    public Weapon(EItemNames name, int value) : base(name, value)
    {
    }
}