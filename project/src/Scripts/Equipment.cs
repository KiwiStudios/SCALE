using System.Collections.Generic;
using SCALE.Enums;

namespace SCALE.Scripts;

public enum EEquipmentField
{
    Helmet = 1,
    ChestPlate = 2,
    Leggings = 3,
    Boots = 4,
    PrimaryWeapon = 5,
    Shield = 6,
}

public class Equipment
{
    public Armour? Helmet;
    public Armour? ChestPlate;
    public Armour? Leggings;
    public Armour? Boots;
    public Weapon? PrimaryWeapon;
    public Shield? Shield;
    public List<Consumable> Consumables;

    public Equipment(List<Consumable> consumables,
                     Weapon primaryWeapon,
                     Armour? helmet,
                     Armour? chestPlate,
                     Armour? leggings,
                     Armour? boots,
                     Shield? shield)
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

    public (Item? item, EEquipmentField field) PickRandomEquipment()
    {
        var num = GD.RandRange(1, 6);
        return num switch
        {
            1 => (Helmet, EEquipmentField.Helmet),
            2 => (ChestPlate, EEquipmentField.ChestPlate),
            3 => (Leggings, EEquipmentField.Leggings),
            4 => (Boots, EEquipmentField.Boots),
            5 => (PrimaryWeapon, EEquipmentField.PrimaryWeapon),
            6 => (Shield, EEquipmentField.Shield),
            _ => throw new ArgumentOutOfRangeException()
        };
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