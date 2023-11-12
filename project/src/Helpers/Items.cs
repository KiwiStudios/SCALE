using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using SCALE.Enums;

namespace SCALE.Helpers;

public static class Items
{

    #region Weapons
    private const int CostOfSimpleWeapon = 20;
    private const int CostOfMagicalWeapon = 55;
    private const int CostOfEnchantedWeapon = 120;
    private const int CostOfWondrousWeapon = 230;
    private const int CostOfGodlyWeapon = 500;

    public static readonly OneHanded SimpleOneHandedWeapon = new OneHanded(EItemNames.OneHanded, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly OneHanded EnchantedOneHandedWeapon = new OneHanded(EItemNames.OneHanded, CostOfMagicalWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly OneHanded MagicalOneHandedWeapon = new OneHanded(EItemNames.OneHanded, CostOfEnchantedWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly OneHanded WondrousOneHandedWeapon = new OneHanded(EItemNames.OneHanded, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly OneHanded GodlyOneHandedWeapon = new OneHanded(EItemNames.OneHanded, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static OneHanded[] TankWeapons = { SimpleOneHandedWeapon, EnchantedOneHandedWeapon, MagicalOneHandedWeapon, WondrousOneHandedWeapon, GodlyOneHandedWeapon };

    public static readonly Shield SimpleShield = new Shield(EItemNames.Shield, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Shield EnchantedShield = new Shield(EItemNames.Shield, CostOfEnchantedWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Shield MagicalShield = new Shield(EItemNames.Shield, CostOfMagicalWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Shield WondrousShield = new Shield(EItemNames.Shield, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Shield GodlyShield = new Shield(EItemNames.Shield, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static Shield[] TankShield = new[] { SimpleShield, EnchantedShield, MagicalShield, WondrousShield, GodlyShield };

    public static readonly TwoHanded SimpleTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly TwoHanded EnchantedTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfEnchantedWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly TwoHanded MagicalTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfMagicalWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly TwoHanded WondrousTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly TwoHanded GodlyTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static TwoHanded[] WarriorWeapons = { SimpleTwoHandedWeapon, EnchantedTwoHandedWeapon, MagicalTwoHandedWeapon, WondrousTwoHandedWeapon, GodlyTwoHandedWeapon };
    
    public static readonly Staff SimpleStaff = new Staff(EItemNames.Staff, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Staff EnchantedStaff = new Staff(EItemNames.Staff, CostOfMagicalWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Staff MagicalStaff = new Staff(EItemNames.Staff, CostOfEnchantedWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Staff WondrousStaff = new Staff(EItemNames.Staff, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Staff GodlyStaff = new Staff(EItemNames.Staff, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static Staff[] SpellCasterWeapons =  { SimpleStaff, EnchantedStaff, MagicalStaff, WondrousStaff, GodlyStaff };
    
    public static readonly Bow SimpleBow = new Bow(EItemNames.Bow, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Bow EnchantedBow = new Bow(EItemNames.Bow, CostOfMagicalWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Bow MagicalBow = new Bow(EItemNames.Bow, CostOfEnchantedWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Bow WondrousBow = new Bow(EItemNames.Bow, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Bow GodlyBow = new Bow(EItemNames.Bow, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static Bow[] ArcherWeapons = { SimpleBow, EnchantedBow, MagicalBow, WondrousBow, GodlyBow };

    #endregion

    #region Armour

    private const int BaseLeatherCost = 20;
    private const int BaseBronzeCost = 55;
    private const int BaseIronCost = 120;
    private const int BaseSteelCost = 230;
    private const int BaseDragonScaleCost = 500;

    private const double HelmetModifier = 1.0;
    private const double ChestPlateModifier = 1.5;
    private const double LeggingsModifier = 1.3;
    private const double BootsModifier = 1.0;

    public static readonly Armour LeatherHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseLeatherCost * HelmetModifier), EArmorMaterial.Leather);
    public static readonly Armour LeatherChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseLeatherCost * ChestPlateModifier), EArmorMaterial.Leather);
    public static readonly Armour LeatherLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseLeatherCost * LeggingsModifier), EArmorMaterial.Leather);
    public static readonly Armour LeatherBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseLeatherCost * BootsModifier), EArmorMaterial.Leather);
    
    public static readonly Armour BronzeHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseBronzeCost * HelmetModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseBronzeCost * ChestPlateModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseBronzeCost * LeggingsModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseBronzeCost * BootsModifier), EArmorMaterial.Bronze);
    
    public static readonly Armour IronHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseIronCost * HelmetModifier), EArmorMaterial.Iron);
    public static readonly Armour IronChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseIronCost * ChestPlateModifier), EArmorMaterial.Iron);
    public static readonly Armour IronLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseIronCost * LeggingsModifier), EArmorMaterial.Iron);
    public static readonly Armour IronBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseIronCost * BootsModifier), EArmorMaterial.Iron);
    
    public static readonly Armour SteelHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseSteelCost * HelmetModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseSteelCost * ChestPlateModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseSteelCost * LeggingsModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseSteelCost * BootsModifier), EArmorMaterial.Steel);
    
    public static readonly Armour DragonScaleHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseDragonScaleCost * HelmetModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScalePlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseDragonScaleCost * ChestPlateModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScaleLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseDragonScaleCost * LeggingsModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScaleBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseDragonScaleCost * BootsModifier), EArmorMaterial.DragonScale);
    
    #endregion

    #region Miscellaneous
    
    public static readonly List<Miscellaneous> MiscItems = new List<Miscellaneous>()
    {
        new Miscellaneous(EItemNames.ElixirOfHealing, 30),
        new Miscellaneous(EItemNames.PotionOfInvisibility, 25),
        new Miscellaneous(EItemNames.ManaCrystal, 20),
        new Miscellaneous(EItemNames.FirebreathAle, 20),
        new Miscellaneous(EItemNames.AmbrosiaNectar, 35),
        new Miscellaneous(EItemNames.DragonsBloodWine, 30),
        new Miscellaneous(EItemNames.TrollbaneTonic, 25),
        new Miscellaneous(EItemNames.FeywildHoney, 25),
        new Miscellaneous(EItemNames.GhostPepperElixir, 20),
        new Miscellaneous(EItemNames.ManticoreMilk, 30),
        new Miscellaneous(EItemNames.MoonlitDewdrops, 25),
        new Miscellaneous(EItemNames.CelestialEssence, 30),
        new Miscellaneous(EItemNames.WyrmwoodBrew, 25),
        new Miscellaneous(EItemNames.PhoenixFeatherTea, 30),
        new Miscellaneous(EItemNames.EnchantedTruffle, 35),
        new Miscellaneous(EItemNames.FaerieFruit, 25),
        new Miscellaneous(EItemNames.BasiliskEyeBrew, 30),
        new Miscellaneous(EItemNames.StardustLoaf, 20),
        new Miscellaneous(EItemNames.OgresStrengthAle, 20),
        new Miscellaneous(EItemNames.SirensSongAle, 20),
        new Miscellaneous(EItemNames.ShadowfireWhiskey, 25),
        new Miscellaneous(EItemNames.GriffinsRoarBrew, 30),
        new Miscellaneous(EItemNames.PixiePlumWine, 25),
        new Miscellaneous(EItemNames.WyrmscaleDraught, 30),
        new Miscellaneous(EItemNames.UnicornTearElixir, 35),
        new Miscellaneous(EItemNames.GorgonsGazeMead, 25),
        new Miscellaneous(EItemNames.NymphsNectar, 30),
        new Miscellaneous(EItemNames.LichbaneElixir, 30),
        new Miscellaneous(EItemNames.GoblinsGoldStout, 25),
        new Miscellaneous(EItemNames.FrostberryNog, 20),
        new Miscellaneous(EItemNames.AngelsKissMead, 25),
        new Miscellaneous(EItemNames.ChimeraChili, 20),
        new Miscellaneous(EItemNames.SorcerersBrew, 25),
        new Miscellaneous(EItemNames.MinotaurMuscleMead, 20),
        new Miscellaneous(EItemNames.StarlightBiscuit, 20),
        new Miscellaneous(EItemNames.GargoyleGravy, 20),
        new Miscellaneous(EItemNames.ElfrootElixir, 25),
        new Miscellaneous(EItemNames.DemonsDelight, 30),
        new Miscellaneous(EItemNames.ValkyriesVigor, 30),
        new Miscellaneous(EItemNames.EnchantersInfusion, 30),
        new Miscellaneous(EItemNames.SphinxsSecret, 35),
        new Miscellaneous(EItemNames.GnomishGrog, 25),
        new Miscellaneous(EItemNames.MoonlitMuffin, 20),
        new Miscellaneous(EItemNames.VipersVenomVial, 30),
        new Miscellaneous(EItemNames.WitchsBrew, 25),
        new Miscellaneous(EItemNames.PaladinsPint, 25),
        new Miscellaneous(EItemNames.GoblinsGrog, 20),
        new Miscellaneous(EItemNames.PegasusPlumBrandy, 25),
        new Miscellaneous(EItemNames.FeywildFizz, 25),
        new Miscellaneous(EItemNames.MoonshadowMintTea, 30),
    };

    #endregion

    #region Collections
    private static List<Item>? _backingGetAllItems;

    private static List<Item> GetAllItemsOnce () 
    {
        var l = new List<Item>(MiscItems);
        _backingGetAllItems ??= l;
        return l;
    }

    public static List<Item> GetAllItems
    {
        get => _backingGetAllItems ?? GetAllItemsOnce();
    }

    public static IEnumerable<Item> RandomItems(int amount)
    {
        return GetAllItems
            .Shuffle()
            .Take(amount);
    }
    

    public static IEnumerable<Miscellaneous> RandomMiscItems(int amount)
    {
        return MiscItems
            .Shuffle()
            .Take(amount);
    }
    
    #endregion
}