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

    private const int StartingItemCount = 6;

    public void InitializeStorage()
    {
        InStorage.AddRange(Items.RandomItems(StartingItemCount));
    }
}