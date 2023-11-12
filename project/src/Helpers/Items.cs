using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using SCALE.Enums;

namespace SCALE.Helpers;

public static class Items
{
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

    public static readonly List<Armour> Armour = new List<Armour>()
    {
        new Armour(EItemNames.LeatherHelmet, 20),
        new Armour(EItemNames.LeatherChestpiece, 35),
        new Armour(EItemNames.LeatherLeggings, 25),
        new Armour(EItemNames.LeatherBoots, 15),
        new Armour(EItemNames.ChainmailCoif, 30),
        new Armour(EItemNames.ChainmailHauberk, 40),
        new Armour(EItemNames.ChainmailLeggings, 35),
        new Armour(EItemNames.ChainmailBoots, 20),
        new Armour(EItemNames.IronHelmet, 40),
        new Armour(EItemNames.IronChestplate, 60),
        new Armour(EItemNames.IronLeggings, 50),
        new Armour(EItemNames.IronBoots, 30),
        new Armour(EItemNames.SteelHelmet, 50),
        new Armour(EItemNames.SteelChestplate, 70),
        new Armour(EItemNames.SteelLeggings, 60),
        new Armour(EItemNames.SteelBoots, 40)
    };

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


    private static List<Item>? _backingGetAllItems;

    private static List<Item> GetAllItemsOnce () 
    {
        var l = new List<Item>(MiscItems);
        l.AddRange(Armour);
        
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

    public static IEnumerable<Armour> RandomGear(int amount)
    {
        return Armour
            .Shuffle()
            .Take(amount);
    }
}