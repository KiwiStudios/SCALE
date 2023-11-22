using System.Linq;
using Environment = System.Environment;

namespace SCALE.Scripts;

public partial class AdventurerPopup : Popup
{
	public Adventurer Adventurer = null!;

	public override void _Ready()
	{
		base._Ready();

		var adventurerDetail = GetNode<RichTextLabel>("CenterContainer/MarginContainer/VBoxContainer/AdventurerDetail");

		adventurerDetail.Text = @$"[font_size={{36}}]{Adventurer.Name}[/font_size]
Gold: {Adventurer.Gold}
Rank: {Adventurer.Rank.ToString()}
Health: {Adventurer.Health}
Class: {Adventurer.Class}

[font_size={{28}}]Statistics[/font_size]
Arcana: {Adventurer.Arcana}
Strength: {Adventurer.Strength}
Agility: {Adventurer.Agility}
Armour Rating: {Adventurer.ArmourRating}

[font_size={{28}}]Equipment[/font_size]
Helmet: {Adventurer.Equipment.Helmet?.DisplayName() ?? "None"}
Chestplate: {Adventurer.Equipment.ChestPlate?.DisplayName() ?? "None"}
Leggings: {Adventurer.Equipment.Leggings?.DisplayName() ?? "None"}
Boots: {Adventurer.Equipment.Boots?.DisplayName() ?? "None"}
Primary weapon: {Adventurer.Equipment.PrimaryWeapon?.DisplayName() ?? "None"}
Shield: {Adventurer.Equipment.Shield?.DisplayName() ?? "None"}

[font_size={{28}}]Consumables[/font_size]
{string.Join(Environment.NewLine, Adventurer.Equipment.Consumables.Select(x => $"{x.DisplayName()}"))}
";

#pragma warning disable CS0219
		var foo = 0;

		Show();
#pragma warning restore CS0219
	}
}
