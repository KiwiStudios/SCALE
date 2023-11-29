using System.Collections.Generic;
using SCALE.Enums;

namespace SCALE.Models;

public class Monster
{
    public EMonsterName Name;
    public EDangerLevel Danger;
    public int Power;
    public Monster(EMonsterName name)
    { 
        Name = name; 
        MonsterStats(name);
    }

    private void MonsterStats(EMonsterName name)
    {
        switch (name)
        {
            case EMonsterName.Goblin:
                SetStats(EDangerLevel.Low, 3);
                break;
            case EMonsterName.Slime:
                SetStats(EDangerLevel.Low, 3);
                break;
            case EMonsterName.Imp:
                SetStats(EDangerLevel.Low, 3);
                break;
            case EMonsterName.Skeleton:
                SetStats(EDangerLevel.Low, 5);
                break;
            case EMonsterName.Zombie:
                SetStats(EDangerLevel.Low, 5);
                break;
            case EMonsterName.GiantRat:
                SetStats(EDangerLevel.Low, 7);
                break;
            case EMonsterName.GiantSpider:
                SetStats(EDangerLevel.Low, 7);
                break;
            case EMonsterName.Harpy:
                SetStats(EDangerLevel.Medium, 10);
                break;
            case EMonsterName.Ogre:
                SetStats(EDangerLevel.Medium, 12);
                break;
            case EMonsterName.Siren:
                SetStats(EDangerLevel.Medium, 14);
                break;
            case EMonsterName.Minotaur:
                SetStats(EDangerLevel.Medium, 20);
                break;
            case EMonsterName.Dragon:
                SetStats(EDangerLevel.High, 40);
                break;
            case EMonsterName.Hydra:
                SetStats(EDangerLevel.High, 45);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(name), name, null);
        }
    }

    private void SetStats(EDangerLevel danger, int power)
    {
        Danger = danger;
        Power = power;
    }

    public string DisplayName()
    {
        return Name switch
        {
            EMonsterName.GiantRat => "Giant Rat",
            EMonsterName.GiantSpider => "Giant Spider",
            _ => Name.ToString()
        };

    }
    
    public string KilledByMessage(bool multiple)
    {
        var preFix = Name switch
        {
            EMonsterName.Goblin => "was struck down by a ",
            EMonsterName.Slime => "was overwhelmed by a ",
            EMonsterName.Skeleton => "was shot down by a ",
            EMonsterName.Zombie => "was mauled to death by a ",
            EMonsterName.GiantRat => "was eating by a ",
            EMonsterName.GiantSpider => "has been cocooned with webs by a ",
            EMonsterName.Imp => "has been stabbed to death by a ",
            EMonsterName.Ogre => "has been mashed to a pulp by a ",
            EMonsterName.Harpy => "has been slashed to bits by a ",
            EMonsterName.Siren => "has been seduced by a ",
            EMonsterName.Minotaur => "has been split in two by a ",
            EMonsterName.Dragon => "was incinerated by a ",
            EMonsterName.Hydra => "has been melted into a puddle by a ",
            _ => throw new ArgumentOutOfRangeException(nameof(Name), Name, null)
        };
        return multiple switch
        {
            true => preFix + "group of " + DisplayName() + "s",
            false => preFix + DisplayName()
        };
    }

    public static Monster Goblin = new Monster(EMonsterName.Goblin);
    public static Monster Slime = new Monster(EMonsterName.Slime);
    public static Monster Skeleton = new Monster(EMonsterName.Skeleton);
    public static Monster Zombie = new Monster(EMonsterName.Zombie);
    public static Monster GiantRat = new Monster(EMonsterName.GiantRat);
    public static Monster GiantSpider = new Monster(EMonsterName.GiantSpider);
    public static Monster Imp = new Monster(EMonsterName.Imp);
    public static Monster Ogre = new Monster(EMonsterName.Ogre);
    public static Monster Harpy = new Monster(EMonsterName.Harpy);
    public static Monster Siren = new Monster(EMonsterName.Siren);
    public static Monster Minotaur = new Monster(EMonsterName.Minotaur);
    public static Monster Dragon = new Monster(EMonsterName.Dragon);
    public static Monster Hydra = new Monster(EMonsterName.Hydra);

    public static List<Monster> LowThreatMonsters = new List<Monster>(){Goblin, Slime, Imp, Skeleton, Zombie, GiantRat, GiantSpider};
    public static List<Monster> MediumThreatMonsters = new List<Monster>() { Ogre, Harpy, Siren, Minotaur };
    public static List<Monster> HighThreatMonsters = new List<Monster>() { Dragon, Hydra };
}