using System.Linq;

namespace SCALE.Scripts;

public partial class DeadAdventurerOverview : VBoxContainer
{
    private EventBus _eventBus = null!;

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
        _eventBus.OnAdventurerDeath += OnAdventurerDeath;
    }

    private void OnAdventurerDeath(Adventurer adventurer)
    {
        AddAdventurerToList(adventurer);
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
}