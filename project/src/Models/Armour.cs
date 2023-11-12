using SCALE.Enums;

namespace SCALE.Models;

public partial class Armour : Item
{
    public EArmour EquipmentSlot;
    public EArmorMaterial Material;
    public ERank Rank;
    public int ArmourRating;
    public Armour(EItemNames name, int value, EArmorMaterial material) : base(name, value)
    {
        Material = material;
        Rank = Material switch
        {

            EArmorMaterial.Leather => ERank.Bronze,
            EArmorMaterial.Bronze => ERank.Silver,
            EArmorMaterial.Iron => ERank.Gold,
            EArmorMaterial.Steel => ERank.Diamond,
            EArmorMaterial.DragonScale => ERank.Legendary,
            _ => throw new ArgumentOutOfRangeException()
        };
        
        switch (name, material)
        {
            case (EItemNames.Helmet, EArmorMaterial.Leather) :
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 1;
                break;
            case (EItemNames.Chestplate, EArmorMaterial.Leather):
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 3;
                break;
            case (EItemNames.Leggings, EArmorMaterial.Leather):
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 2;
                break;
            case (EItemNames.Boots, EArmorMaterial.Leather):
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 1;
                break;
            case (EItemNames.Helmet, EArmorMaterial.Bronze):
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 3;
                break;
            case (EItemNames.Chestplate, EArmorMaterial.Bronze):
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 5;
                break;
            case (EItemNames.Leggings, EArmorMaterial.Bronze):
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 4;
                break;
            case (EItemNames.Boots, EArmorMaterial.Bronze):
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 3;
                break;
            case (EItemNames.Helmet, EArmorMaterial.Iron):
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 6;
                break;
            case (EItemNames.Chestplate, EArmorMaterial.Iron):
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 8;
                break;
            case (EItemNames.Leggings, EArmorMaterial.Iron):
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 7;
                break;
            case (EItemNames.Boots, EArmorMaterial.Iron):
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 6;
                break;
            case (EItemNames.Helmet, EArmorMaterial.Steel):
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 10;
                break;
            case (EItemNames.Chestplate, EArmorMaterial.Steel):
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 12;
                break;
            case (EItemNames.Leggings, EArmorMaterial.Steel):
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 11;
                break;
            case (EItemNames.Boots, EArmorMaterial.Steel):
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 10;
                break;
            case (EItemNames.Helmet, EArmorMaterial.DragonScale):
                EquipmentSlot = EArmour.Helmet;
                ArmourRating = 15;
                break;
            case (EItemNames.Chestplate, EArmorMaterial.DragonScale):
                EquipmentSlot = EArmour.Chestplate;
                ArmourRating = 17;
                break;
            case (EItemNames.Leggings, EArmorMaterial.DragonScale):
                EquipmentSlot = EArmour.Leggings;
                ArmourRating = 16;
                break;
            case (EItemNames.Boots, EArmorMaterial.DragonScale):
                EquipmentSlot = EArmour.Boots;
                ArmourRating = 15;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
    }
}