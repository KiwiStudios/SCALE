using System.Collections.Generic;
using SCALE.Enums;

namespace SCALE.Scripts;

public class Equipment
{
    public Armour? Helmet;
    public Armour? ChestPlate;
    public Armour? Leggings;
    public Armour? Boots;
    public Weapon? PrimaryWeapon;
    public Shield? Shield;
    public List<Miscellaneous> Consumables;
    
    public Equipment(List<Miscellaneous> consumables, Weapon primaryWeapon, Armour? helmet, Armour? chestPlate, Armour? leggings, Armour? boots, Shield? shield)
    {
        if (helmet is not null && helmet.EquipmentSlot != EArmour.Helmet)
        {
            throw new Exception("Not a helmet");
        }
        if (chestPlate is not null && chestPlate.EquipmentSlot != EArmour.Chestplate)
        {
            throw new Exception("Not a chestplate");
        }
        if (leggings is not null && leggings.EquipmentSlot != EArmour.Leggings)
        {
            throw new Exception("Not a legging");
        }
        if (boots is not null && boots.EquipmentSlot != EArmour.Boots)
        {
            throw new Exception("Not shoes");
        }

        Helmet = helmet;
        ChestPlate = chestPlate;
        Leggings = leggings;
        Boots = boots;
        PrimaryWeapon = primaryWeapon;
        Shield = shield;
        Consumables = consumables;
    }
    
    
    public int DetermineArmourRating()
    {
        var rating = 0;
        rating += Helmet?.ArmourRating ?? 0;
        rating += ChestPlate?.ArmourRating ?? 0;
        rating += Leggings?.ArmourRating ?? 0;
        rating += Boots?.ArmourRating ?? 0;
        rating += Shield?.ArmourRating ?? 0;
        return rating;

    }
}