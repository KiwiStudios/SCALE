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

    public string DisplayName(EMonsterName name)
    {
        return name switch
        {
            EMonsterName.GiantRat => "Giant Rat",
            EMonsterName.GiantSpider => "Giant Spider",
            _ => name.ToString()
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
            true => preFix + "group of " + DisplayName(Name) + "s",
            false => preFix + DisplayName(Name)
        };
    }
}