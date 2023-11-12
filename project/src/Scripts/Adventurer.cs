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
        var startingConsumables = Items.RandomMiscItems(2);
        var weapon = DetermineStartingWeapon();
        var helmet = FindStartingArmourPiece(EArmour.Helmet);
        var chestPlate = FindStartingArmourPiece(EArmour.Chestplate);
        var leggings = FindStartingArmourPiece(EArmour.Leggings);
        var boots = FindStartingArmourPiece(EArmour.Boots);
        var shield = Class is EAdventureClass.Tank ? FindStartingShield() : null;

        return new Equipment(startingConsumables.ToList(), weapon, helmet, chestPlate, leggings, boots, shield);
    }

    private Armour? FindStartingArmourPiece(EArmour slot)
    {
        var onPar = GD.RandRange(0, 100) > 40;
        if (Rank is ERank.Bronze && !onPar)
        {
            return null;
        }
        var rankToUse = onPar ? Rank : (ERank)((int)Rank - 1);
        return rankToUse switch
        {
            ERank.Bronze => slot switch
            {
                EArmour.Helmet => Items.LeatherHelmet,
                EArmour.Chestplate => Items.LeatherChestPlate,
                EArmour.Leggings => Items.LeatherLeggings,
                EArmour.Boots => Items.LeatherBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Silver => slot switch
            {
                EArmour.Helmet => Items.BronzeHelmet,
                EArmour.Chestplate => Items.BronzeChestPlate,
                EArmour.Leggings => Items.BronzeLeggings,
                EArmour.Boots => Items.BronzeBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Gold => slot switch
            {
                EArmour.Helmet => Items.IronHelmet,
                EArmour.Chestplate => Items.IronChestPlate,
                EArmour.Leggings => Items.IronLeggings,
                EArmour.Boots => Items.IronBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Diamond => slot switch
            {
                EArmour.Helmet => Items.SteelHelmet,
                EArmour.Chestplate => Items.SteelChestPlate,
                EArmour.Leggings => Items.SteelLeggings,
                EArmour.Boots => Items.SteelBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            ERank.Legendary => slot switch
            {
                EArmour.Helmet => Items.DragonScaleHelmet,
                EArmour.Chestplate => Items.DragonScalePlate,
                EArmour.Leggings => Items.DragonScaleLeggings,
                EArmour.Boots => Items.DragonScaleBoots,
                _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
            },
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private Shield FindStartingShield()
    {
        return Rank switch
        {

            ERank.Bronze => Items.SimpleShield,
            ERank.Silver => Items.EnchantedShield,
            ERank.Gold => Items.MagicalShield,
            ERank.Diamond => Items.WondrousShield,
            ERank.Legendary => Items.GodlyShield,
            _ => throw new ArgumentOutOfRangeException()
        };
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