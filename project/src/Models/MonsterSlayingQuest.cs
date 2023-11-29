using System.Collections.Generic;
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
            ERank.Silver => BronzeSlayingQuest(),
            ERank.Gold => BronzeSlayingQuest(),
            ERank.Diamond => BronzeSlayingQuest(),
            ERank.Legendary => BronzeSlayingQuest(),
            _ => throw new ArgumentOutOfRangeException(nameof(rank), rank, null)
        };
    }


    private Stack<Monster> BronzeSlayingQuest()
    {
        int encounter = GD.RandRange(0, 1);
        switch(encounter)
        {
            case 0:
                Monsters.Push(new Monster(EMonsterName.Zombie));
                break;
            case 1: 
                Monsters.Push(new Monster(EMonsterName.Goblin));
                break;
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
        return "went on a quest to slay";
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