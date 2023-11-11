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
            case EAdventureClass.Healer:
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
        return GD.RandRange(1, 15) * ((int)Rank + 1);
    }
    private int HighStat()
    {
        return GD.RandRange(10, 20) * ((int)Rank + 1);
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
        var weapon = Items.RandomWeapon();
        if (Class is not (EAdventureClass.Spellcaster or EAdventureClass.Healer)) return weapon;
        return Rank switch
        {
            ERank.Bronze => Items.SimpleStaff,
            ERank.Silver => Items.EnchantedStaff,
            ERank.Gold => Items.MagicalStaff,
            ERank.Diamond => Items.WondrousStaff,
            ERank.Legendary => Items.GoldyStaff,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}