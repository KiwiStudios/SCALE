using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using SCALE.Enums;

namespace SCALE.Helpers;

public static class Items
{
    public static readonly List<Weapon> Weapons = new List<Weapon>()
    {
        new Weapon(EItemNames.Longsword, 50),
        new Weapon(EItemNames.Warhammer, 55),
        new Weapon(EItemNames.Crossbow, 45),
        new Weapon(EItemNames.Mace, 50),
        new Weapon(EItemNames.Halberd, 60),
        new Weapon(EItemNames.Dagger, 35),
        new Weapon(EItemNames.Flail, 55),
        new Weapon(EItemNames.Poleaxe, 65),
        new Weapon(EItemNames.Battleaxe, 60),
        new Weapon(EItemNames.Morningstar, 55),
        new Weapon(EItemNames.Greatsword, 70),
        new Weapon(EItemNames.Lance, 60),
        new Weapon(EItemNames.Quarterstaff, 40),
        new Weapon(EItemNames.Javelin, 45),
        new Weapon(EItemNames.Rapier, 45),
        new Weapon(EItemNames.Bow, 45),
        new Weapon(EItemNames.Shield, 30),
        new Weapon(EItemNames.Scimitar, 50),
        new Weapon(EItemNames.Falchion, 50),
    };

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
        l.AddRange(Weapons);
        
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
}