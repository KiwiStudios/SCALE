using SCALE.Scripts.Managers;

namespace SCALE.Scripts;

public partial class AdventurerOverview : VBoxContainer
{
    private StoreManager _storeManager = null!;
    private AdventurerManager _adventurerManager = null!;
    private EventBus _eventBus = null!;

    public override void _Ready()
    {
        base._Ready();

        _storeManager = (StoreManager)Root.SceneTree.GetFirstNodeInGroup(StoreManager.GroupName);
        _adventurerManager = (AdventurerManager)Root.SceneTree.GetFirstNodeInGroup(AdventurerManager.GroupName);
        _eventBus = this.GetEventBus();

        _eventBus.OnItemSold += OnItemSold; 
        _eventBus.OnAdventurerDeath += OnAdventurerDeath;
        
        Render();
    }

    private void OnAdventurerDeath(Adventurer adventurer)
    {
        Rerender();
    }
    

    private void OnItemSold(Item item, Adventurer adventurer)
    {
        Rerender();
    }

    private void Rerender()
    {
        foreach (var child in GetChildren())
        {
            child.Free();
        }

        _storeManager = (StoreManager)Root.SceneTree.GetFirstNodeInGroup(StoreManager.GroupName);
        _adventurerManager = (AdventurerManager)Root.SceneTree.GetFirstNodeInGroup(AdventurerManager.GroupName);
        Render();
    }

    private void Render()
    {
        var topRow = CreateTopRow();
        AddChild(topRow);
        MoveChild(topRow, 0);

        foreach (var adventurer in _adventurerManager.Adventurers)
        {
            var row = CreateRow();
            var adventurerText = new AdventurerText()
            {
                AutowrapMode = TextServer.AutowrapMode.Off,
                FitContent = true,
                Adventurer = adventurer,
                AnimateText = false,
                CustomMinimumSize = new Vector2(100, 20)
            };
            adventurerText.BackingText = $"{adventurer.Name}";
            adventurerText.Text = $"{adventurer.Name}";
            adventurerText.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.ColourCode()));
            row.AddChild(adventurerText);

            var helmet = CreateCell(adventurer.Equipment.Helmet?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.Helmet is not null) helmet.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.Helmet?.Rank.GetColourCode()));
            row.AddChild(helmet);

            var chestPlate = CreateCell(adventurer.Equipment.ChestPlate?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.ChestPlate is not null) chestPlate.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.ChestPlate?.Rank.GetColourCode()));
            row.AddChild(chestPlate);

            var leggings = CreateCell(adventurer.Equipment.Leggings?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.Leggings is not null) leggings.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.Leggings?.Rank.GetColourCode()));
            row.AddChild(leggings);

            var boots = CreateCell(adventurer.Equipment.Boots?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.Boots is not null) boots.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.Boots?.Rank.GetColourCode()));
            row.AddChild(boots);

            var primaryWeapon = CreateCell(adventurer.Equipment.PrimaryWeapon?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.PrimaryWeapon is not null)
                primaryWeapon.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.PrimaryWeapon?.Rank.GetColourCode()));
            row.AddChild(primaryWeapon);

            var shield = CreateCell(adventurer.Equipment.Shield?.Rank.ToString() ?? "X");
            if (adventurer.Equipment.Shield is not null) shield.AddThemeColorOverride("default_color", Color.FromHtml(adventurer.Equipment.Shield?.Rank.GetColourCode()));
            row.AddChild(shield);

            AddChild(row);
            MoveChild(row, 1);
        }
    }

    private GridContainer CreateTopRow()
    {
        var topRow = CreateRow();

        topRow.AddChild(CreateCell("Adventurer"));
        topRow.AddChild(CreateCell("Helmet"));
        topRow.AddChild(CreateCell("Chestplate"));
        topRow.AddChild(CreateCell("Leggings"));
        topRow.AddChild(CreateCell("Boots"));
        topRow.AddChild(CreateCell("Weapon"));
        topRow.AddChild(CreateCell("Shield"));

        return topRow;
    }

    private RichTextLabel CreateCell(string text)
    {
        return new RichTextLabel()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = false,
            Text = text,
            CustomMinimumSize = new Vector2(100, 20)
        };
    }

    public GridContainer CreateRow()
    {
        return new GridContainer()
        {
            GrowHorizontal = GrowDirection.Both,
            GrowVertical = GrowDirection.Both,
            SizeFlagsHorizontal = SizeFlags.ExpandFill,
            Columns = 7
        };
    }
}