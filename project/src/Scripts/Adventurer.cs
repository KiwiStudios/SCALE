using System.Collections.Generic;
using System.Diagnostics;
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
    public int Arcana;
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
        ArmourRating = Equipment.DetermineArmourRating();
    }

    public float GetTotalAdventurerScore()
    {
        return (int)Rank
            + Equipment.DetermineArmourRating()
            + Equipment.PrimaryWeapon?.WeaponRating ?? 0;
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
        Arcana = AverageStat();
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
                Arcana = HighStat();
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
            ERank.Bronze => GD.RandRange(1, 8),
            ERank.Silver => GD.RandRange(3, 10),
            ERank.Gold => GD.RandRange(5, 12),
            ERank.Diamond => GD.RandRange(8, 15),
            ERank.Legendary => GD.RandRange(10, 20),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private int HighStat()
    {
        return Rank switch
        {
            ERank.Bronze => GD.RandRange(8, 12),
            ERank.Silver => GD.RandRange(10, 14),
            ERank.Gold => GD.RandRange(12, 16),
            ERank.Diamond => GD.RandRange(15, 20),
            ERank.Legendary => GD.RandRange(20, 25),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Equipment DetermineStartingEquipment()
    {
        var startingConsumables = Items.FindPotions(2, Rank);
        var weapon = Items.FindWeapon(Class, Rank);
        var helmet = FindStartingArmourPiece(EArmour.Helmet);
        var chestPlate = FindStartingArmourPiece(EArmour.Chestplate);
        var leggings = FindStartingArmourPiece(EArmour.Leggings);
        var boots = FindStartingArmourPiece(EArmour.Boots);
        var shield = Class is EAdventureClass.Tank ? Items.FindShield(Rank) : null;

        return new Equipment(startingConsumables.ToList(), weapon, helmet, chestPlate, leggings, boots, shield);
    }

    private Armour? FindStartingArmourPiece(EArmour slot)
    {
        //Usually adventurers have the gear around their level, chance they have one tier lower
        var onPar = GD.RandRange(0, 100) > 40;
        if (Rank is ERank.Bronze && !onPar)
        {
            return null;
        }

        var rankToUse = onPar ? Rank : (ERank)((int)Rank - 1);
        return Items.FindArmourPiece(rankToUse, slot);
    }

    public string ColourCode() => Rank.GetColourCode();
}