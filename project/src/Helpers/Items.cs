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
    public static readonly OneHanded[] TankWeapons = { SimpleOneHandedWeapon, EnchantedOneHandedWeapon, MagicalOneHandedWeapon, WondrousOneHandedWeapon, GodlyOneHandedWeapon };

    public static readonly Shield SimpleShield = new Shield(EItemNames.Shield, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Shield EnchantedShield = new Shield(EItemNames.Shield, CostOfEnchantedWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Shield MagicalShield = new Shield(EItemNames.Shield, CostOfMagicalWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Shield WondrousShield = new Shield(EItemNames.Shield, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Shield GodlyShield = new Shield(EItemNames.Shield, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static readonly Shield[] AllShields = new[] { SimpleShield, EnchantedShield, MagicalShield, WondrousShield, GodlyShield };

    public static readonly TwoHanded SimpleTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly TwoHanded EnchantedTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfEnchantedWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly TwoHanded MagicalTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfMagicalWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly TwoHanded WondrousTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly TwoHanded GodlyTwoHandedWeapon = new TwoHanded(EItemNames.TwoHanded, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static readonly TwoHanded[] WarriorWeapons = { SimpleTwoHandedWeapon, EnchantedTwoHandedWeapon, MagicalTwoHandedWeapon, WondrousTwoHandedWeapon, GodlyTwoHandedWeapon };

    public static readonly Staff SimpleStaff = new Staff(EItemNames.Staff, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Staff EnchantedStaff = new Staff(EItemNames.Staff, CostOfMagicalWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Staff MagicalStaff = new Staff(EItemNames.Staff, CostOfEnchantedWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Staff WondrousStaff = new Staff(EItemNames.Staff, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Staff GodlyStaff = new Staff(EItemNames.Staff, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static readonly Staff[] SpellCasterWeapons = { SimpleStaff, EnchantedStaff, MagicalStaff, WondrousStaff, GodlyStaff };

    public static readonly Bow SimpleBow = new Bow(EItemNames.Bow, CostOfSimpleWeapon, ERank.Bronze, EItemLevel.Simple);
    public static readonly Bow EnchantedBow = new Bow(EItemNames.Bow, CostOfMagicalWeapon, ERank.Silver, EItemLevel.Enchanted);
    public static readonly Bow MagicalBow = new Bow(EItemNames.Bow, CostOfEnchantedWeapon, ERank.Gold, EItemLevel.Magical);
    public static readonly Bow WondrousBow = new Bow(EItemNames.Bow, CostOfWondrousWeapon, ERank.Diamond, EItemLevel.Wondrous);
    public static readonly Bow GodlyBow = new Bow(EItemNames.Bow, CostOfGodlyWeapon, ERank.Legendary, EItemLevel.Godly);
    public static readonly Bow[] ArcherWeapons = { SimpleBow, EnchantedBow, MagicalBow, WondrousBow, GodlyBow };

    public static readonly Weapon[] SimpleWeapons = { SimpleOneHandedWeapon, SimpleTwoHandedWeapon, SimpleStaff, SimpleBow };
    public static readonly Weapon[] EnchantedWeapons = { EnchantedOneHandedWeapon, EnchantedTwoHandedWeapon, EnchantedStaff, EnchantedBow };
    public static readonly Weapon[] MagicalWeapons = { MagicalOneHandedWeapon, MagicalTwoHandedWeapon, MagicalStaff, MagicalBow };
    public static readonly Weapon[] WondrousWeapons = { WondrousOneHandedWeapon, WondrousTwoHandedWeapon, WondrousStaff, WondrousBow };
    public static readonly Weapon[] GodlyWeapons = { GodlyOneHandedWeapon, GodlyTwoHandedWeapon, GodlyStaff, GodlyBow };

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
    public static readonly Armour[] LeatherArmour = { LeatherHelmet, LeatherChestPlate, LeatherLeggings, LeatherBoots };

    public static readonly Armour BronzeHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseBronzeCost * HelmetModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseBronzeCost * ChestPlateModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseBronzeCost * LeggingsModifier), EArmorMaterial.Bronze);
    public static readonly Armour BronzeBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseBronzeCost * BootsModifier), EArmorMaterial.Bronze);
    public static readonly Armour[] BronzeArmour = { BronzeHelmet, BronzeChestPlate, BronzeLeggings, BronzeBoots };

    public static readonly Armour IronHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseIronCost * HelmetModifier), EArmorMaterial.Iron);
    public static readonly Armour IronChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseIronCost * ChestPlateModifier), EArmorMaterial.Iron);
    public static readonly Armour IronLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseIronCost * LeggingsModifier), EArmorMaterial.Iron);
    public static readonly Armour IronBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseIronCost * BootsModifier), EArmorMaterial.Iron);
    public static readonly Armour[] IronArmour = { IronHelmet, IronChestPlate, IronLeggings, IronBoots };

    public static readonly Armour SteelHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseSteelCost * HelmetModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelChestPlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseSteelCost * ChestPlateModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseSteelCost * LeggingsModifier), EArmorMaterial.Steel);
    public static readonly Armour SteelBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseSteelCost * BootsModifier), EArmorMaterial.Steel);
    public static readonly Armour[] SteelArmour = { SteelHelmet, SteelChestPlate, SteelLeggings, SteelBoots };

    public static readonly Armour DragonScaleHelmet = new Armour(EItemNames.Helmet, (int)Math.Round(BaseDragonScaleCost * HelmetModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScalePlate = new Armour(EItemNames.Chestplate, (int)Math.Round(BaseDragonScaleCost * ChestPlateModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScaleLeggings = new Armour(EItemNames.Leggings, (int)Math.Round(BaseDragonScaleCost * LeggingsModifier), EArmorMaterial.DragonScale);
    public static readonly Armour DragonScaleBoots = new Armour(EItemNames.Boots, (int)Math.Round(BaseDragonScaleCost * BootsModifier), EArmorMaterial.DragonScale);
    public static readonly Armour[] DragonScaleArmour = { DragonScaleHelmet, DragonScalePlate, DragonScaleLeggings, DragonScaleBoots };

    #endregion

    #region Consumables

    private const int SmallPotionCost = 10;
    private const int MediumPotionCost = 25;
    private const int LargePotionCost = 50;

    public static readonly Consumable SmallHealthPotion = new Consumable(EItemNames.Potion, SmallPotionCost, EPotionTypes.Health, EVialSize.Small);
    public static readonly Consumable MediumHealthPotion = new Consumable(EItemNames.Potion, MediumPotionCost, EPotionTypes.Health, EVialSize.Medium);
    public static readonly Consumable LargeHealthPotion = new Consumable(EItemNames.Potion, LargePotionCost, EPotionTypes.Health, EVialSize.Large);
    public static readonly Consumable[] HealthPotions = { SmallHealthPotion, MediumHealthPotion, LargeHealthPotion };

    public static readonly Consumable SmallStaminaPotion = new Consumable(EItemNames.Potion, SmallPotionCost, EPotionTypes.Stamina, EVialSize.Small);
    public static readonly Consumable MediumStaminaPotion = new Consumable(EItemNames.Potion, MediumPotionCost, EPotionTypes.Stamina, EVialSize.Medium);
    public static readonly Consumable LargeStaminaPotion = new Consumable(EItemNames.Potion, LargePotionCost, EPotionTypes.Stamina, EVialSize.Large);
    public static readonly Consumable[] StaminaPotions = { SmallStaminaPotion, MediumStaminaPotion, LargeStaminaPotion };

    public static readonly Consumable SmallManaPotion = new Consumable(EItemNames.Potion, SmallPotionCost, EPotionTypes.Mana, EVialSize.Small);
    public static readonly Consumable MediumManaPotion = new Consumable(EItemNames.Potion, MediumPotionCost, EPotionTypes.Mana, EVialSize.Medium);
    public static readonly Consumable LargeManaPotion = new Consumable(EItemNames.Potion, LargePotionCost, EPotionTypes.Mana, EVialSize.Large);
    public static readonly Consumable[] ManaPotions = { SmallManaPotion, MediumManaPotion, LargeManaPotion };

    public static readonly Consumable[] SmallPotions = { SmallHealthPotion, SmallStaminaPotion, SmallManaPotion };
    public static readonly Consumable[] MediumPotions = { MediumHealthPotion, MediumStaminaPotion, MediumManaPotion };
    public static readonly Consumable[] LargePotions = { LargeHealthPotion, LargeStaminaPotion, LargeManaPotion };
    
    #endregion

    #region Miscellaneous

    /// <summary>
    /// Commented out for now but if we implement crafting we probably want to use it for raw crafting materials
    /// </summary>
    
    // public static readonly List<Miscellaneous> MiscItems = new List<Miscellaneous>
    // {
    //     new Miscellaneous(EItemNames.PotionOfInvisibility, 25),
    //     new Miscellaneous(EItemNames.ManaCrystal, 20),
    //     new Miscellaneous(EItemNames.FirebreathAle, 20),
    //     new Miscellaneous(EItemNames.AmbrosiaNectar, 35),
    //     new Miscellaneous(EItemNames.DragonsBloodWine, 30),
    //     new Miscellaneous(EItemNames.TrollbaneTonic, 25),
    //     new Miscellaneous(EItemNames.FeywildHoney, 25),
    //     new Miscellaneous(EItemNames.GhostPepperElixir, 20),
    //     new Miscellaneous(EItemNames.ManticoreMilk, 30),
    //     new Miscellaneous(EItemNames.MoonlitDewdrops, 25),
    //     new Miscellaneous(EItemNames.CelestialEssence, 30),
    //     new Miscellaneous(EItemNames.WyrmwoodBrew, 25),
    //     new Miscellaneous(EItemNames.PhoenixFeatherTea, 30),
    //     new Miscellaneous(EItemNames.EnchantedTruffle, 35),
    //     new Miscellaneous(EItemNames.FaerieFruit, 25),
    //     new Miscellaneous(EItemNames.BasiliskEyeBrew, 30),
    //     new Miscellaneous(EItemNames.StardustLoaf, 20),
    //     new Miscellaneous(EItemNames.OgresStrengthAle, 20),
    //     new Miscellaneous(EItemNames.SirensSongAle, 20),
    //     new Miscellaneous(EItemNames.ShadowfireWhiskey, 25),
    //     new Miscellaneous(EItemNames.GriffinsRoarBrew, 30),
    //     new Miscellaneous(EItemNames.PixiePlumWine, 25),
    //     new Miscellaneous(EItemNames.WyrmscaleDraught, 30),
    //     new Miscellaneous(EItemNames.UnicornTearElixir, 35),
    //     new Miscellaneous(EItemNames.GorgonsGazeMead, 25),
    //     new Miscellaneous(EItemNames.NymphsNectar, 30),
    //     new Miscellaneous(EItemNames.LichbaneElixir, 30),
    //     new Miscellaneous(EItemNames.GoblinsGoldStout, 25),
    //     new Miscellaneous(EItemNames.FrostberryNog, 20),
    //     new Miscellaneous(EItemNames.AngelsKissMead, 25),
    //     new Miscellaneous(EItemNames.ChimeraChili, 20),
    //     new Miscellaneous(EItemNames.SorcerersBrew, 25),
    //     new Miscellaneous(EItemNames.MinotaurMuscleMead, 20),
    //     new Miscellaneous(EItemNames.StarlightBiscuit, 20),
    //     new Miscellaneous(EItemNames.GargoyleGravy, 20),
    //     new Miscellaneous(EItemNames.ElfrootElixir, 25),
    //     new Miscellaneous(EItemNames.DemonsDelight, 30),
    //     new Miscellaneous(EItemNames.ValkyriesVigor, 30),
    //     new Miscellaneous(EItemNames.EnchantersInfusion, 30),
    //     new Miscellaneous(EItemNames.SphinxsSecret, 35),
    //     new Miscellaneous(EItemNames.GnomishGrog, 25),
    //     new Miscellaneous(EItemNames.MoonlitMuffin, 20),
    //     new Miscellaneous(EItemNames.VipersVenomVial, 30),
    //     new Miscellaneous(EItemNames.WitchsBrew, 25),
    //     new Miscellaneous(EItemNames.PaladinsPint, 25),
    //     new Miscellaneous(EItemNames.GoblinsGrog, 20),
    //     new Miscellaneous(EItemNames.PegasusPlumBrandy, 25),
    //     new Miscellaneous(EItemNames.FeywildFizz, 25),
    //     new Miscellaneous(EItemNames.MoonshadowMintTea, 30),
    // };

    #endregion

    #region Collections

    private static List<Weapon>? _backingAllWeapons;
    private static List<Armour>? _backingAllArmour;
    private static List<Consumable>? _backingAllPotions;
    private static List<Item>? _backingGetAllItems;
    private static List<Item>? _backAllBronzeRankItems;
    private static List<Item>? _backAllSilverRankItems;
    private static List<Item>? _backAllGoldRankItems;
    private static List<Item>? _backAllDiamondRankItems;
    private static List<Item>? _backAllLegendaryRankItems;

    private static List<Weapon> GetAllWeaponsOnce()
    {
        var l = new List<Weapon>();
        l.AddRange(TankWeapons);
        l.AddRange(WarriorWeapons);
        l.AddRange(SpellCasterWeapons);
        l.AddRange(ArcherWeapons);
        _backingAllWeapons ??= l;
        return l;
    }

    private static List<Weapon> AllWeapons
    {
        get => _backingAllWeapons ?? GetAllWeaponsOnce();
    }
    
    private static List<Consumable> GetAllPotionsOnce()
    {
        var l = new List<Consumable>();
        l.AddRange(HealthPotions);
        l.AddRange(ManaPotions);
        l.AddRange(StaminaPotions);
        _backingAllPotions ??= l;
        return l;
    }

    private static List<Consumable> AllPotions
    {
        get => _backingAllPotions ?? GetAllPotionsOnce();
    }

    private static List<Armour> GetAllArmourOnce()
    {
        var l = new List<Armour>();
        l.AddRange(LeatherArmour);
        l.AddRange(BronzeArmour);
        l.AddRange(IronArmour);
        l.AddRange(SteelArmour);
        l.AddRange(DragonScaleArmour);
        _backingAllArmour ??= l;
        return l;
    }

    private static List<Armour> AllArmour
    {
        get => _backingAllArmour ?? GetAllArmourOnce();
    }


    private static List<Item> GetAllBronzeItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(SimpleWeapons);
        l.Add(SimpleShield);
        l.AddRange(LeatherArmour);
        l.AddRange(SmallPotions);
        _backAllBronzeRankItems ??= l;
        return l;
    }

    public static List<Item> AllBronzeRankItems
    {
        get => _backAllBronzeRankItems ?? GetAllBronzeItemsOnce();
    }

    private static List<Item> GetAllSilverItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(EnchantedWeapons);
        l.Add(EnchantedShield);
        l.AddRange(BronzeArmour);
        l.AddRange(SmallPotions);
        _backAllSilverRankItems ??= l;
        return l;
    }

    public static List<Item> AllSilverRankItems
    {
        get => _backAllSilverRankItems ?? GetAllSilverItemsOnce();
    }

    private static List<Item> GetAllGoldItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(MagicalWeapons);
        l.Add(MagicalShield);
        l.AddRange(IronArmour);
        l.AddRange(MediumPotions);
        _backAllGoldRankItems ??= l;
        return l;
    }

    public static List<Item> AllGoldRankItems
    {
        get => _backAllGoldRankItems ?? GetAllGoldItemsOnce();
    }

    private static List<Item> GetAllDiamondItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(WondrousWeapons);
        l.Add(WondrousShield);
        l.AddRange(SteelArmour);
        l.AddRange(MediumPotions);
        _backAllDiamondRankItems ??= l;
        return l;
    }

    public static List<Item> AllDiamondRankItems
    {
        get => _backAllDiamondRankItems ?? GetAllDiamondItemsOnce();
    }

    private static List<Item> GetAllLegendaryItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(GodlyWeapons);
        l.Add(GodlyShield);
        l.AddRange(DragonScaleArmour);
        l.AddRange(LargePotions);
        _backAllLegendaryRankItems ??= l;
        return l;
    }

    public static List<Item> AllLegendaryRankItems
    {
        get => _backAllLegendaryRankItems ?? GetAllLegendaryItemsOnce();
    }

    private static List<Item> GetAllItemsOnce()
    {
        var l = new List<Item>();
        l.AddRange(AllWeapons);
        l.AddRange(AllShields);
        l.AddRange(AllArmour);
        l.AddRange(AllPotions);
        _backingGetAllItems ??= l;
        return l;
    }

    public static List<Item> AllItems
    {
        get => _backingGetAllItems ?? GetAllItemsOnce();
    }

    #endregion

    #region RandomItems

    public static Consumable RandomSmallPotion()
    {
        var potion = SmallPotions
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (potion is null) throw new Exception("No potion found");
        return potion;
    }
    
    public static Consumable RandomMediumPotion()
    {
        var potion = MediumPotions
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (potion is null) throw new Exception("No potion found");
        return potion;
    }
    
    public static Consumable RandomLargePotion()
    {
        var potion = LargePotions
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (potion is null) throw new Exception("No potion found");
        return potion;
    }

    public static Item RandomBronzeRankItem()
    {
        var item = AllBronzeRankItems
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (item is null) throw new Exception("No Bronze Rank Item Found");
        return item;
    }

    public static Item RandomSilverRankItem()
    {
        var item = AllSilverRankItems
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (item is null) throw new Exception("No Silver Rank Item Found");
        return item;
    }

    public static Item RandomGoldRankItem()
    {
        var item = AllGoldRankItems
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (item is null) throw new Exception("No Gold Rank Item Found");
        return item;
    }


    public static Item RandomDiamondRankItem()
    {
        var item = AllDiamondRankItems
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (item is null) throw new Exception("No Diamond Rank Item Found");
        return item;
    }

    public static Item RandomLegendaryRankItem()
    {
        var item = AllLegendaryRankItems
            .Shuffle()
            .Take(1)
            .ToArray()
            [0];
        if (item is null) throw new Exception("No Legendary Rank Item Found");
        return item;
    }

    public static IEnumerable<Item> RandomBronzeRankItems(int amount)
    {
        return AllBronzeRankItems
            .Shuffle()
            .Take(amount);
    }

    public static IEnumerable<Item> RandomSilverRankItems(int amount)
    {
        return AllSilverRankItems
            .Shuffle()
            .Take(amount);
    }

    public static IEnumerable<Item> RandomGoldRankItems(int amount)
    {
        return AllGoldRankItems
            .Shuffle()
            .Take(amount);
    }

    public static IEnumerable<Item> RandomDiamondRankItems(int amount)
    {
        return AllDiamondRankItems
            .Shuffle()
            .Take(amount);
    }

    public static IEnumerable<Item> RandomLegendaryRankItems(int amount)
    {
        return AllLegendaryRankItems
            .Shuffle()
            .Take(amount);
    }
    
    public static Consumable[] RandomSmallPotions(int amount)
    {
        List<Consumable> potions = new List<Consumable>();
        for (int i = 0; i < amount; i++)
        {
            var randomIndex = GD.RandRange(0, SmallPotions.Length - 1);
            potions.Add(SmallPotions[randomIndex]);
        }
        return potions.ToArray();
    }
    
    public static Consumable[] RandomMediumPotions(int amount)
    {
        List<Consumable> potions = new List<Consumable>();
        for (int i = 0; i < amount; i++)
        {
            var randomIndex = GD.RandRange(0, MediumPotions.Length - 1);
            potions.Add(MediumPotions[randomIndex]);
        }
        return potions.ToArray();
    }
    
    
    public static Consumable[] RandomLargePotions(int amount)
    {
        List<Consumable> potions = new List<Consumable>();
        for (int i = 0; i < amount; i++)
        {
            var randomIndex = GD.RandRange(0, LargePotions.Length - 1);
            potions.Add(LargePotions[randomIndex]);
        }
        return potions.ToArray();
    }
    
    #endregion


    #region Functions

    public static Armour FindArmourPiece(ERank rank, EArmour slot)
    {
        return rank switch
        {
            ERank.Bronze => slot switch
            {
                EArmour.Helmet => LeatherHelmet,
                EArmour.Chestplate => LeatherChestPlate,
                EArmour.Leggings => LeatherLeggings,
                EArmour.Boots => LeatherBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Silver => slot switch
            {
                EArmour.Helmet => BronzeHelmet,
                EArmour.Chestplate => BronzeChestPlate,
                EArmour.Leggings => BronzeLeggings,
                EArmour.Boots => BronzeBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Gold => slot switch
            {
                EArmour.Helmet => IronHelmet,
                EArmour.Chestplate => IronChestPlate,
                EArmour.Leggings => IronLeggings,
                EArmour.Boots => IronBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Diamond => slot switch
            {
                EArmour.Helmet => SteelHelmet,
                EArmour.Chestplate => SteelChestPlate,
                EArmour.Leggings => SteelLeggings,
                EArmour.Boots => SteelBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Legendary => slot switch
            {
                EArmour.Helmet => DragonScaleHelmet,
                EArmour.Chestplate => DragonScalePlate,
                EArmour.Leggings => DragonScaleLeggings,
                EArmour.Boots => DragonScaleBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Weapon FindWeapon(EAdventureClass adventureClass, ERank rank)
    {
        return adventureClass switch
        {
            (EAdventureClass.Spellcaster) => rank switch
            {
                ERank.Bronze => SimpleStaff,
                ERank.Silver => EnchantedStaff,
                ERank.Gold => MagicalStaff,
                ERank.Diamond => WondrousStaff,
                ERank.Legendary => GodlyStaff,
                _ => throw new ArgumentOutOfRangeException()
            },
            (EAdventureClass.Archer) => rank switch
            {
                ERank.Bronze => SimpleBow,
                ERank.Silver => EnchantedBow,
                ERank.Gold => MagicalBow,
                ERank.Diamond => WondrousBow,
                ERank.Legendary => GodlyBow,
                _ => throw new ArgumentOutOfRangeException()
            },
            EAdventureClass.Warrior => rank switch
            {
                ERank.Bronze => SimpleTwoHandedWeapon,
                ERank.Silver => EnchantedTwoHandedWeapon,
                ERank.Gold => MagicalTwoHandedWeapon,
                ERank.Diamond => WondrousOneHandedWeapon,
                ERank.Legendary => GodlyTwoHandedWeapon,
                _ => throw new ArgumentOutOfRangeException()
            },
            EAdventureClass.Tank => rank switch
            {
                ERank.Bronze => SimpleOneHandedWeapon,
                ERank.Silver => EnchantedOneHandedWeapon,
                ERank.Gold => MagicalOneHandedWeapon,
                ERank.Diamond => WondrousOneHandedWeapon,
                ERank.Legendary => GodlyOneHandedWeapon,
                _ => throw new ArgumentOutOfRangeException()
            },
            _ => throw new Exception("Unkown class")
        };
    }

    public static Shield FindShield(ERank rank)
    {
        return rank switch
        {
            ERank.Bronze => SimpleShield,
            ERank.Silver => EnchantedShield,
            ERank.Gold => MagicalShield,
            ERank.Diamond => WondrousShield,
            ERank.Legendary => GodlyShield,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static Consumable FindPotion(ERank rank)
    {
        return rank switch
        {
            ERank.Bronze => RandomSmallPotion(),
            ERank.Silver => RandomSmallPotion(),
            ERank.Gold => RandomMediumPotion(),
            ERank.Diamond => RandomMediumPotion(),
            ERank.Legendary => RandomLargePotion(),
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }
    
    public static Consumable[] FindPotions(int amount, ERank rank)
    {
        return rank switch
        {
            ERank.Bronze => RandomSmallPotions(amount),
            ERank.Silver => RandomSmallPotions(amount),
            ERank.Gold => RandomMediumPotions(amount),
            ERank.Diamond => RandomMediumPotions(amount),
            ERank.Legendary => RandomLargePotions(amount),
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }
    #endregion
}