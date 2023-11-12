using System.Collections.Generic;
using System.IO;
using System.Linq;
using SCALE.Scripts.Managers;
using StringyEnums;
using Environment = System.Environment;

namespace SCALE.Scripts;

public partial class EventLogLabel : RichTextLabel
{
    private EventBus _eventBus = null!;
    private List<string> textToWrite = new List<string>();
    private double deltaSum = 0;
    private double writeSpeed = TimeManager.Threshold / 20; // per second

    public override void _EnterTree()
    {
        base._EnterTree();

        _eventBus = this.GetEventBus();
 
        _eventBus.OnItemSold += OnItemSold;
    }

    private void OnItemSold(Item item, Adventurer adventurer)
    {
        var colour = adventurer.ColourCode();
        AddEvent(
            $"{item.DisplayName()} was sold to [color={colour}]{adventurer.Name}[/color] for {item.Value}"
        );
    }

    public void AddEvent(string text)
    {
        textToWrite.Add("NEW_ITEM");
        textToWrite.Add(text);
    }


    public override void _Process(double delta)
    {
        deltaSum += delta;

        if (deltaSum > writeSpeed)
        {
            deltaSum = 0;
            if (textToWrite.Count > 0)
            {
                if (textToWrite[0] == "NEW_ITEM")
                {
                    textToWrite.RemoveAt(0);
                    Text = Environment.NewLine + Text;
                    return;
                }

                if (textToWrite[0].Length == 0)
                {
                    textToWrite.RemoveAt(0);
                    return;
                }
                
                Text = textToWrite[0][^1] + Text;
                textToWrite[0] = textToWrite[0].Remove(textToWrite[0].Length - 1);
            }
        }
    }
}