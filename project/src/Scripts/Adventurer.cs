using System.Collections.Generic;
using System.Linq;
using Bogus;
using SCALE.Enums;
using SCALE.Helpers;

namespace SCALE.Scripts;

public partial class Adventurer : RefCounted
{
    public EAdventureClass Class;
    public string Name;
    public ERank Rank;
    public int Health;
    public int Intelligence;
    public int Strength;
    public int Agility;
    public int Gold;
    public int ArmourRating;
    public Equipment Equipment;

    private readonly List<string> _locales = new List<string>()
    {
        "en",
        "de",
        "it",
        "fr",
        "nl",
        "es",
        "pl"
    };

    public Adventurer()
    {
        var randomLocale = _locales[GD.RandRange(0, _locales.Count - 1)];
        Name = new Faker(randomLocale).Name.FirstName();
        Class = Extensions.GetRandomEnumValue<EAdventureClass>();
        Rank = DetermineRank();
        GenerateInitialStats();
        Equipment = DetermineStartingEquipment();
        ArmourRating = DetermineArmourRating();
    }

    private ERank DetermineRank()
    {
        var randomNumber = GD.RandRange(0, 100);
        return randomNumber switch
        {
            < 45 => ERank.Bronze,
            < 70 => ERank.Silver,
            < 85 => ERank.Gold,
            < 95 => ERank.Diamond,
            _ => ERank.Legendary
        };
    }
    
    private void GenerateInitialStats()
    {
        Intelligence = AverageStat();
        Strength = AverageStat();
        Agility = AverageStat();
        Health = AverageStat();

        //Make main stat for class better
        switch (Class)
        {
            case EAdventureClass.Warrior:
                Strength = HighStat();
                break;
            case EAdventureClass.Archer:
                Agility = HighStat();
                break;
            case EAdventureClass.Spellcaster:
                Intelligence = HighStat();
                break;
            case EAdventureClass.Tank:
                Health = HighStat();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private int AverageStat()
    {
        return Rank switch
        {
            ERank.Bronze => GD.RandRange(1,8),
            ERank.Silver => GD.RandRange(3,10),
            ERank.Gold => GD.RandRange(5,12),
            ERank.Diamond => GD.RandRange(8,15),
            ERank.Legendary => GD.RandRange(10,20),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    private int HighStat()
    {
        return Rank switch
        {
            ERank.Bronze => GD.RandRange(8,12),
            ERank.Silver => GD.RandRange(10,14),
            ERank.Gold => GD.RandRange(12,16),
            ERank.Diamond => GD.RandRange(15,20),
            ERank.Legendary => GD.RandRange(20,25),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Equipment DetermineStartingEquipment()
    {
        var startingConsumables = Items.RandomMiscItems(2);
        var weapon = DetermineStartingWeapon();
        var gear = Items.RandomGear(3).ToList();
        var helmet = gear.Find(piece => piece.EquipmentSlot == EArmour.Helmet);
        var chestPlate = gear.Find(piece => piece.EquipmentSlot == EArmour.Chestplate);
        var leggings = gear.Find(piece => piece.EquipmentSlot == EArmour.Leggings);
        var boots = gear.Find(piece => piece.EquipmentSlot == EArmour.Boots);
        
        return new Equipment(startingConsumables.ToList(), helmet, chestPlate, leggings, boots, weapon);
    }

    private int DetermineArmourRating()
    {
        var rating = 0;
        rating += Equipment.Helmet?.ArmourRating ?? 0;
        rating += Equipment.ChestPlate?.ArmourRating ?? 0;
        rating += Equipment.Leggings?.ArmourRating ?? 0;
        rating += Equipment.Boots?.ArmourRating ?? 0;
        return rating;

    }

    private Weapon DetermineStartingWeapon()
    {
        switch (Class)
        {
            case (EAdventureClass.Spellcaster):
                return Rank switch
                {
                    ERank.Bronze => Items.SimpleStaff,
                    ERank.Silver => Items.EnchantedStaff,
                    ERank.Gold => Items.MagicalStaff,
                    ERank.Diamond => Items.WondrousStaff,
                    ERank.Legendary => Items.GodlyStaff,
                    _ => throw new ArgumentOutOfRangeException()
                };
            case (EAdventureClass.Archer):
                return Rank switch
                {
                    ERank.Bronze => Items.SimpleBow,
                    ERank.Silver => Items.EnchantedBow,
                    ERank.Gold => Items.MagicalBow,
                    ERank.Diamond => Items.WondrousBow,
                    ERank.Legendary => Items.GodlyBow,
                    _ => throw new ArgumentOutOfRangeException()
                };
            case EAdventureClass.Warrior:
                return Rank switch
                {
                    ERank.Bronze => Items.SimpleTwoHandedWeapon,
                    ERank.Silver => Items.EnchantedTwoHandedWeapon,
                    ERank.Gold => Items.MagicalTwoHandedWeapon,
                    ERank.Diamond => Items.WondrousOneHandedWeapon,
                    ERank.Legendary => Items.GodlyTwoHandedWeapon,
                    _ => throw new ArgumentOutOfRangeException()
                };
            case EAdventureClass.Tank:
                return Rank switch
                {
                    ERank.Bronze => Items.SimpleOneHandedWeapon,
                    ERank.Silver => Items.EnchantedOneHandedWeapon,
                    ERank.Gold => Items.MagicalOneHandedWeapon,
                    ERank.Diamond => Items.WondrousOneHandedWeapon,
                    ERank.Legendary => Items.GodlyOneHandedWeapon,
                    _ => throw new ArgumentOutOfRangeException()
                };
            default:
            {
                throw new Exception("Unkown class");
            }
        }

    }
}