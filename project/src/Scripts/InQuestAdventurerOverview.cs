using System.Linq;

namespace SCALE.Scripts;

public partial class InQuestAdventurerOverview : VBoxContainer
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
        _eventBus.OnAdventurerGoesOnQuest += OnAdventurerGoesOnQuest;
        _eventBus.OnAdventurerComesBackFromQuest += OnAdventurerComesBackFromQuest;
        _eventBus.OnAdventurerDeath += OnAdventurerDeath;
    }

    private void OnAdventurerDeath(Adventurer adventurer)
    {
        RemoveAdventurerFromList(adventurer);
    }

    private void AddAdventurerToList(Adventurer adventurer)
    {
        foreach (var child in GetChildren())
        {
            if (child is Label) continue;

            var adventurerText = (AdventurerText)child;
            if (adventurerText.Adventurer.Id == adventurer.Id)
            {
                return;
            }
        }

        var adventurerName = new AdventurerText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
            Adventurer = adventurer,
            Text = adventurer.Name
        };
        adventurerName.AnimateText = false;
        adventurerName.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Rank.GetColourCode()));

        AddChild(adventurerName);
    }

    private void RemoveAdventurerFromList(Adventurer adventurer)
    {
        var childs = GetChildren().ToList();

        var child = childs.FirstOrDefault(child => child is not Label && ((AdventurerText)child).Adventurer.Id == adventurer.Id);
        if (child is not (AdventurerText)default)
        {
            child.QueueFree();
            return;
        }
    }

    private void OnAdventurerComesBackFromQuest(Adventurer adventurer, Quest quest)
    {
        RemoveAdventurerFromList(adventurer);
    }

    private void OnAdventurerGoesOnQuest(Adventurer adventurer, Quest quest)
    {
        AddAdventurerToList(adventurer);
    }
}