using SCALE.Enums;

namespace SCALE.Models;

public partial class Armour : Item
{
    public EArmour EquipmentSlot;
    public int ArmourRating;
    public Armour(EItemNames name, int value) : base(name, value)
    {
        switch (name)
        {
            case EItemNames.LeatherHelmet:
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 1;
                break;
            case EItemNames.LeatherChestpiece:
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 3;
                break;
            case EItemNames.LeatherLeggings:
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 2;
                break;
            case EItemNames.LeatherBoots:
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 1;
                break;
            case EItemNames.ChainmailCoif:
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 2;
                break;
            case EItemNames.ChainmailHauberk:
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 4;
                break;
            case EItemNames.ChainmailLeggings:
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 3;
                break;
            case EItemNames.ChainmailBoots:
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 2;
                break;
            case EItemNames.IronHelmet:
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 4;
                break;
            case EItemNames.IronChestplate:
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 6;
                break;
            case EItemNames.IronLeggings:
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 5;
                break;
            case EItemNames.IronBoots:
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 4;
                break;
            case EItemNames.SteelHelmet:
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 5;
                break;
            case EItemNames.SteelChestplate:
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 8;
                break;
            case EItemNames.SteelLeggings:
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 7;
                break;
            case EItemNames.SteelBoots:
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 5;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
    }
}