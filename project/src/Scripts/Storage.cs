using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using SCALE.Helpers;

namespace SCALE.Scripts;

public class Storage
{

    public Storage()
    {
        InitializeStorage();
    }
    public List<Item> InStorage { get; set; } = new List<Item>();

    private const int StartingItemCountBronzeRank = 5;
    private const int StartingItemCountSilverRank = 3;
    private const int StartingItemCountConsumable = 4;
    

    public void InitializeStorage()
    {
        InStorage.AddRange(Items.RandomBronzeRankItems(StartingItemCountBronzeRank));
        InStorage.AddRange(Items.RandomSilverRankItems(StartingItemCountSilverRank));
        InStorage.AddRange(Items.RandomMiscItems(StartingItemCountConsumable));
    }
}