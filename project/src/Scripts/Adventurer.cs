using System.Collections.Generic;
using Bogus;
using SCALE.Helpers;

namespace SCALE.Scripts;

public partial class Adventurer : RefCounted
{
    public string Name;
    public List<Item> Inventory = new List<Item>();
    private readonly List<string> _locales = new List<string>()
    {
        "en",
        "de",
        "it",
        "uk",
        "fr",
        "nl",
        "es",
        "pl"
    };
    
    public Adventurer()
    {
        var startingItems = Items.RandomItems(3);
        Inventory.AddRange(startingItems);
        var randomLocale = _locales[GD.RandRange(0, _locales.Count - 1)];
        Name = new Faker(randomLocale).Name.FirstName();
    }
}