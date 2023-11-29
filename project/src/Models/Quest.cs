using SCALE.Enums;
using SCALE.Scripts.Managers;

namespace SCALE.Models;

public abstract partial class Quest : RefCounted
{
    public EQuestTypes Type;
    public int TravelTime;
    public int OldTime; //In minutes
    public bool QuestCompleted;
    public int QuestCompletionTime;

    protected Quest(ERank rank, EQuestTypes questType)
    {
        Type = questType;
    }

    public static Quest RandomQuest(ERank rank)
    {
        var questType = Extensions.GetRandomEnumValue<EQuestTypes>();
        var quest = questType switch
        {
            EQuestTypes.MonsterSlaying => new MonsterSlayingQuest(rank),
            _ => throw new ArgumentOutOfRangeException()
        };
        quest.TravelTime = GD.RandRange(120, 180);
        return quest;
    }

    public void ProgressTime(Adventurer adventurer, int timeElapsed)
    {
        var newTime = OldTime + timeElapsed;

        for (int i = 0; i < (timeElapsed / TimeManager.TickTime); i++)
        {
            var currentTime = OldTime + (i * TimeManager.TickTime);
            TimeTick(currentTime, adventurer);
        }

        if (QuestCompleted && newTime > QuestCompletionTime + TravelTime)
        {
            adventurer.Quest = null;
            Root.EventBus.EmitOnAdventurerComesBackFromQuest(adventurer, this);
            return;
        }

        OldTime += timeElapsed;
    }

    private void TimeTick(int currentTime, Adventurer adventurer)
    {
        //If not yet arrived on quest location do nothing
        if (currentTime < TravelTime) return;

        //Do nothing when the quest is completed as traveling back
        if (QuestCompleted) return;

        ProgressQuest(adventurer, currentTime);
    }

    protected abstract void ProgressQuest(Adventurer adventurer, int currentTime);

    public abstract string Text();
}