using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using SCALE.Enums;

namespace SCALE.Models;

public partial class MonsterSlayingQuest : Quest
{
    private readonly Stack<Monster> _monsters = new Stack<Monster>();
    private List<Monster> _monstersAtStartOfFight = new List<Monster>();
    private int _damageDuringQuest;
    public MonsterSlayingQuest(ERank rank) : base(rank, EQuestTypes.MonsterSlaying)
    {
        DetermineMonsters(rank);
    }
    
    public override string Text()
    {
        var monsterName = _monstersAtStartOfFight.First().Name.ToString();
        var prefix = "a quest to slay a ";
        if (_monstersAtStartOfFight.Count > 1)
        {
            prefix += "a group of ";
            monsterName += "s";
        }
        return prefix + monsterName;
    }
    
    private void DetermineMonsters(ERank rank)
    {
        var t = rank switch
        {
            ERank.Bronze => BronzeSlayingQuest(),
            ERank.Silver => SilverSlayingQuest(),
            ERank.Gold => GoldSlayingQuest(),
            ERank.Diamond => DiamondSlayingQuest(),
            ERank.Legendary => LegendarySlayingQuest(),
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
        _monstersAtStartOfFight = t.ToList();
    }


    private Stack<Monster> BronzeSlayingQuest()
    {
        var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
        _monsters.Push(monsterToFight);
        return _monsters;
    }

    private Stack<Monster> SilverSlayingQuest()
    {
        var quantity = GD.RandRange(1, 3);
        var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
        for (int i = 0; i < quantity; i++)
        {
            _monsters.Push(monsterToFight);
        }

        return _monsters;
    }

    private Stack<Monster> GoldSlayingQuest()
    {
        var roll = GD.RandRange(0, 1);
        var challenge = roll == 1 ? EDangerLevel.Low : EDangerLevel.Medium;
        if (challenge is EDangerLevel.Low)
        {
            var quantity = GD.RandRange(3, 5);
            var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
            for (int i = 0; i < quantity; i++)
            {
                _monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.MediumThreatMonsters);
            _monsters.Push(monsterToFight);
        }

        return _monsters;
    }
    
    private Stack<Monster> DiamondSlayingQuest()
    {
        var challenge = Extensions.GetRandomEnumValue<EDangerLevel>();
        if (challenge is EDangerLevel.Low)
        {
            var quantity = GD.RandRange(7, 10);
            var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
            for (int i = 0; i < quantity; i++)
            {
                _monsters.Push(monsterToFight);
            }
        }
        else if(challenge is EDangerLevel.Medium)
        {
            var quantity = GD.RandRange(1, 4);
            var monsterToFight = Extensions.RandomItemFromList(Monster.MediumThreatMonsters);
            for (int i = 0; i < quantity; i++)
            {
                _monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.HighThreatMonsters);
            _monsters.Push(monsterToFight);
        }

        return _monsters;
    }
    
    
    private Stack<Monster> LegendarySlayingQuest()
    {
        var roll = GD.RandRange(0, 1);
        var challenge = roll == 1 ? EDangerLevel.Medium : EDangerLevel.High;
        if(challenge is EDangerLevel.Medium)
        {
            var quantity = GD.RandRange(3, 6);
            var monsterToFight = Extensions.RandomItemFromList(Monster.MediumThreatMonsters);
            for (int i = 0; i < quantity; i++)
            {
                _monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.HighThreatMonsters);
            _monsters.Push(monsterToFight);
        }

        return _monsters;
    }
    
    
    protected override void ProgressQuest(Adventurer adventurer, int currentTime)
    {
        if (_monsters.Count <= 0) return;
        
        var monster = _monsters.Peek();
        MonsterAttack(adventurer, monster);

        if (_damageDuringQuest >= adventurer.MaxHealth)
        {
            adventurer.IsDead = true;
            adventurer.DeathMessage = monster.KilledByMessage(_monsters.Count > 1);
            return;
        }

        if (!AdventurerKillsMonster(adventurer)) return;
        
        _monsters.Pop();
        
        if (_monsters.Count == 0)
        {
            QuestCompleted = true;
            QuestCompletionTime = currentTime;
        }

    }

    private void MonsterAttack(Adventurer adventurer, Monster monster)
    {
        var toHit = GD.RandRange(0, 100);
        
        //Better agility allows you to dodge more easily
        if (toHit < adventurer.Agility * 4) return;

        var t = 1 - (double)adventurer.ArmourRating / 100;
        _damageDuringQuest += (int)(monster.Power * t);
    }

    private bool AdventurerKillsMonster(Adventurer adventurer)
    {
        var neededToKill = GD.RandRange(0, 20);
        return adventurer.Strength >= neededToKill;
    }
}