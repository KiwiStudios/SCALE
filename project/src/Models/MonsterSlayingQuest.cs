using System.Collections;
using System.Collections.Generic;
using MoreLinq.Extensions;
using SCALE.Enums;

namespace SCALE.Models;

public partial class MonsterSlayingQuest : Quest
{
    private Stack<Monster> Monsters = new Stack<Monster>();
    private int DamageDuringQuest;
    public MonsterSlayingQuest(ERank rank) : base(rank, EQuestTypes.MonsterSlaying)
    {
        DetermineMonsters(rank);
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
    }


    private Stack<Monster> BronzeSlayingQuest()
    {
        var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
        Monsters.Push(monsterToFight);
        return Monsters;
    }

    private Stack<Monster> SilverSlayingQuest()
    {
        var quantity = GD.RandRange(1, 3);
        var monsterToFight = Extensions.RandomItemFromList(Monster.LowThreatMonsters);
        for (int i = 0; i < quantity; i++)
        {
            Monsters.Push(monsterToFight);
        }

        return Monsters;
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
                Monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.MediumThreatMonsters);
            Monsters.Push(monsterToFight);
        }

        return Monsters;
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
                Monsters.Push(monsterToFight);
            }
        }
        else if(challenge is EDangerLevel.Medium)
        {
            var quantity = GD.RandRange(1, 4);
            var monsterToFight = Extensions.RandomItemFromList(Monster.MediumThreatMonsters);
            for (int i = 0; i < quantity; i++)
            {
                Monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.HighThreatMonsters);
            Monsters.Push(monsterToFight);
        }

        return Monsters;
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
                Monsters.Push(monsterToFight);
            }
        }
        else
        {
            var monsterToFight = Extensions.RandomItemFromList(Monster.HighThreatMonsters);
            Monsters.Push(monsterToFight);
        }

        return Monsters;
    }
    
    
    protected override void ProgressQuest(Adventurer adventurer)
    {
        if (Monsters.Count <= 0) return;
        
        var monster = Monsters.Peek();
        MonsterAttack(adventurer, monster);

        if (DamageDuringQuest >= adventurer.Health)
        {
            adventurer.IsDead = true;
            adventurer.DeathMessage = monster.KilledByMessage(Monsters.Count > 1);
            return;
        }

        if (!AdventurerKillsMonster(adventurer)) return;
        
        Monsters.Pop();
        
        if (Monsters.Count == 0)
        {
            QuestCompleted = true;
        }

    }
    public override string Text()
    {
        var monsterName = Monsters.Peek().Name.ToString();
        var prefix = "went on a quest to slay a ";
        if (Monsters.Count > 1)
        {
            prefix += "a group of ";
            monsterName += "s";
        }
        return prefix + monsterName;
    }

    private void MonsterAttack(Adventurer adventurer, Monster monster)
    {
        var toHit = GD.RandRange(0, 100);
        if (toHit <= adventurer.ArmourRating) return;
        DamageDuringQuest += monster.Power;
    }

    private bool AdventurerKillsMonster(Adventurer adventurer)
    {
        var neededToKill = GD.RandRange(0, 20);
        return adventurer.Strength >= neededToKill;
    }
}