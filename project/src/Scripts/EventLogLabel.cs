﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MoreLinq;
using MoreLinq.Extensions;
using SCALE.Enums;
using SCALE.Scripts.Managers;
using StringyEnums;
using Environment = System.Environment;

namespace SCALE.Scripts;

public partial class EventLogLabel : VBoxContainer
{
    private EventBus _eventBus = null!;
    private List<List<EventLogLabelText>?> rowsToWrite = new List<List<EventLogLabelText>?>();
    private int currentIdx = 0;
    private int currentRowChildIdx = 0;
    private double deltaSum = 0;
    private double writeSpeed = TimeManager.Threshold / 35; // per second

    private List<string> OnItemSoldTemplates = new List<string>()
    {
        "An adventurous soul named [Name] snagged [ItemName] for a mere [ItemValue] coins!",
        "For a bargain of [ItemValue] coins, [ItemName] found a new home with [Name], an eager buyer.",
        "[ItemName] changed hands in exchange for [ItemValue] coins to the delight of [Name].",
        "[ItemValue] coins lighter, [Name] proudly claims [ItemName] as their own.",
        "In a marketplace deal, [ItemName] became [Name]'s prized possession for [ItemValue] coins.",
        "For the sum of [ItemValue] coins, [ItemName] journeyed into the hands of [Name], an ambitious buyer.",
        "Luck shone bright for [Name] as they secured [ItemName] for a mere [ItemValue] coins.",
        "At a steal of [ItemValue] coins, [ItemName] now aids [Name] on their daring quests.",
        "[ItemName]'s price tag? A modest [ItemValue] coins, claimed by [Name].",
        "[ItemValue] coins sealed the deal - [ItemName] is now trusted by [Name].",
        "In the bustling market, [Name] invested [ItemValue] coins to acquire [ItemName].",
        "A wallet lightened by [ItemValue] coins, all for the glory of [Name] wielding [ItemName].",
        "For the price of [ItemValue] coins, [ItemName] found a new master in [Name], a daring buyer.",
        "[ItemName] fetched a reasonable [ItemValue] coins from [Name]'s eager purse.",
        "[ItemValue] coins later, [ItemName] proudly hangs at [Name]'s side.",
        "[Name]'s eyes gleamed upon securing [ItemName] for just [ItemValue] coins.",
        "In the trade of coins for glory, [Name] claimed [ItemName] for [ItemValue] coins.",
        "[ItemName]'s new owner? [Name], who gladly parted with [ItemValue] coins.",
        "For the sum of [ItemValue] coins, [ItemName] embraced its new fate with [Name]'s spirit.",
        "[Name]'s bargain - [ItemName] acquired for a mere [ItemValue] coins.",
        "In the exchange of wealth and valor, [ItemName] found a new home with [Name] for [ItemValue] coins.",
        "For the modest price of [ItemValue] coins, [ItemName] now aids [Name].",
        "[ItemName]'s price? A steal at [ItemValue] coins, claimed by [Name].",
        "In the market's hubbub, [Name] scored [ItemName] for just [ItemValue] coins.",
        "[ItemValue] coins richer, [Name] triumphantly wields [ItemName] on their journeys.",
    };

    List<string> SplitStringBasedOnIdentifiers(string inputString)
    {
        List<string> splitList = new List<string>();

        string pattern = @"(\[ItemValue\]|\[ItemName\]|\[Name\])";
        string[] substrings = Regex.Split(inputString, pattern);

        foreach (string match in substrings)
        {
            if (!string.IsNullOrWhiteSpace(match))
            {
                splitList.Add(match);
            }
        }

        return splitList;
    }

    public List<object> GetRandomTextOnItemSold(EventLogLabelText adventurer,
                                                Item item)
    {
        var template = OnItemSoldTemplates[GD.RandRange(0, OnItemSoldTemplates.Count - 1)];
        var output = new List<object>();

        var splittedStrings = SplitStringBasedOnIdentifiers(template);

        foreach (var splittedString in splittedStrings)
        {
            if (splittedString == "[Name]")
            {
                output.Add(adventurer);
                continue;
            }

            if (splittedString == "[ItemName]")
            {
                var label = new EventLogLabelText()
                {
                    AutowrapMode = TextServer.AutowrapMode.Off,
                    FitContent = true,
                };
                label.BackingText = $"{item.DisplayName()}";
                label.AddThemeColorOverride("default_color", Color.FromHtml(GetColourCodeFromItem(item)));
                output.Add(label);
                continue;
            }

            if (splittedString == "[ItemValue]")
            {
                var label = new EventLogLabelText()
                {
                    AutowrapMode = TextServer.AutowrapMode.Off,
                    FitContent = true,
                };
                label.BackingText = $"{item.Value} gold";
                label.AddThemeColorOverride("default_color", Color.FromHtml("#FFD700"));
                output.Add(label);
                continue;
            }
            
            output.Add(splittedString);
        }

        return output;
    }

    private string GetColourCodeFromItem(Item item)
    {
        var rank = item switch
        {
            Weapon weapon => weapon.Rank,
            Armour armour => armour.Rank,
            Consumable consumable => consumable.GetRank(),
            _ => throw new ArgumentOutOfRangeException(nameof(item), item, null)
        };

        return rank.GetColourCode();
    }
    
    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();

        _eventBus.OnItemSold += OnItemSold;
        _eventBus.OnAdventurerDeath += OnAdventureDeath;
        _eventBus.OnAdventurerLevelUp += OnAdventurerLevelUp;
        _eventBus.OnAdventurerComesBackFromQuest += OnAdventurerComesBackFromQuest;
    }

    private void OnAdventurerComesBackFromQuest(Adventurer adventurer, Quest quest)
    {
        var colour = adventurer.ColourCode();
        var adventurerName = new AdventurerText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
            Adventurer = adventurer
        };
        adventurerName.BackingText = adventurer.Name;
        adventurerName.AddThemeColorOverride("default_color", Color.FromHtml(colour));
        AddEvent(
            new List<object>()
            {
                adventurerName,
                $" ",
                "has come back from",
                quest.Text(),
            }
        );
    }

    private void OnAdventureDeath(Adventurer adventurer)
    {
        var colour = adventurer.ColourCode();
        var adventurerName = new AdventurerText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
            Adventurer = adventurer
        };
        adventurerName.BackingText = $"{adventurer.Name}";
        adventurerName.AddThemeColorOverride("default_color", Color.FromHtml(colour));

        AddEvent(
            new List<object>()
            {
                adventurerName,
                $" ",
                adventurer.DeathMessage!,
                "!"
            }
        );
    }

    private void OnAdventurerLevelUp(ERank rankfrom,
                                     ERank rankto,
                                     Adventurer adventurer)
    {
        var colour = adventurer.ColourCode();

        var adventurerName = new AdventurerText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
            Adventurer = adventurer
        };
        adventurerName.BackingText = $"{adventurer.Name}";
        adventurerName.AddThemeColorOverride("default_color", Color.FromHtml(colour));

        var rankFromText = new EventLogLabelText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
        };
        rankFromText.BackingText = $"{rankfrom}";
        rankFromText.AddThemeColorOverride("default_color", Color.FromHtml(rankfrom.GetColourCode()));

        var rankToText = new EventLogLabelText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
        };
        rankToText.BackingText = $"{rankto}";
        rankToText.AddThemeColorOverride("default_color", Color.FromHtml(rankto.GetColourCode()));

        AddEvent(
            new List<object>()
            {
                adventurerName,
                $" leveled up from ",
                rankFromText,
                " to ",
                rankToText,
                "!"
            }
        );
    }

    private void OnItemSold(Item item, Adventurer adventurer)
    {
        var colour = adventurer.ColourCode();

        var adventurerName = new AdventurerText()
        {
            AutowrapMode = TextServer.AutowrapMode.Off,
            FitContent = true,
            Adventurer = adventurer
        };
        adventurerName.BackingText = $"{adventurer.Name}";
        adventurerName.AddThemeColorOverride("default_color", Color.FromHtml(colour));

        AddEvent(
            GetRandomTextOnItemSold(adventurerName, item)
        );
    }

    public void AddEvent(List<object> text)
    {
        var row = new List<EventLogLabelText>();

        foreach (var t in text)
        {
            if (t is string s)
            {
                var ellt = new EventLogLabelText()
                {
                    AutowrapMode = TextServer.AutowrapMode.Off,
                    FitContent = true,
                };
                ellt.BackingText = s;
                row.Add(
                    ellt
                );
            }

            if (t is EventLogLabelText r)
            {
                row.Add(
                    r
                );
            }
        }

        rowsToWrite.Add(row);
    }

    private HBoxContainer? currentRow = null;

    public override void _Process(double delta)
    {
        deltaSum += delta;

        if (deltaSum > writeSpeed)
        {
            deltaSum = 0;
            if (HasRowsToWrite())
            {
                if (currentRow.IsNotValid())
                {
                    currentRow = new ParagraphContainer()
                    {
                        GrowHorizontal = GrowDirection.Both,
                        GrowVertical = GrowDirection.Both
                    };
                    AddChild(currentRow);
                    MoveChild(currentRow, 0);
                }

                var child = rowsToWrite[currentIdx]![currentRowChildIdx];
                var isWriting = child.BackingText.Length > 0;
                var isOverMaxWidth = currentRow.GetChildren().Cast<EventLogLabelText>().Sum(x => x.Size.X) > Math.Max(currentRow.Size.X - 25, 25);

                if (currentRow.GetChildren().Contains(child) is false)
                {
                    currentRow.AddChild(child);
                }


                if (isOverMaxWidth)
                {
                    var indexOfCurrentRowInChildren = GetChildren().ToList().FindIndex(x => x.NativeInstance == currentRow.NativeInstance);
                    var innerRow = new List<EventLogLabelText>();
                    innerRow.AddRange(
                        rowsToWrite[currentIdx]!.Skip(currentRowChildIdx + 1)
                    );

                    // add copy of element that was being written to the start of the new row
                    var elementBeingWritten = (EventLogLabelText)rowsToWrite[currentIdx]![currentRowChildIdx];
                    var backingText = elementBeingWritten.BackingText;
                    var duplicate = (EventLogLabelText)elementBeingWritten.Duplicate();

                    duplicate.BackingText = backingText.TrimStart();

                    innerRow.Insert(0, duplicate);

                    var firstNonNullIdx = rowsToWrite.FindIndex(x => x is not null);
                    firstNonNullIdx += 1;

                    rowsToWrite.Insert(firstNonNullIdx, innerRow);

                    GotoNextRow();

                    currentRow = new ParagraphContainer()
                    {
                        GrowHorizontal = GrowDirection.Both,
                        GrowVertical = GrowDirection.Both,
                    };
                    AddChild(currentRow);
                    MoveChild(currentRow, indexOfCurrentRowInChildren + 1);

                    return;
                }

                if (isWriting)
                {
                    child.Text += child.BackingText[0].ToString();
                    child.BackingText = child.BackingText[1..];
                    return;
                }
                else
                {
                    if (rowsToWrite[currentIdx]!.Count - 1 > currentRowChildIdx)
                    {
                        currentRowChildIdx++;
                        return;
                    }

                    if (currentRowChildIdx == rowsToWrite[currentIdx]!.Count - 1)
                    {
                        GotoNextRow();
                        return;
                    }
                }
            }
        }
    }

    private bool HasRowsToWrite()
    {
        return rowsToWrite.Count(x => x is not null) > 0;
    }

    private void GotoNextRow()
    {
        rowsToWrite[currentIdx] = null;
        currentIdx++;
        currentRowChildIdx = 0;
        currentRow = null;
    }
}