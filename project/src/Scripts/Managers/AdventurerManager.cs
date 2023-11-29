using System.Collections.Generic;
using System.Linq;
using SCALE.Enums;

namespace SCALE.Scripts.Managers;

public partial class AdventurerManager : Node
{
    private EventBus _eventBus = null!;
    private const int AdventureCount = 20;
    private const int QuestChance = 40;
    private StoreManager _storeManager = null!;

    public readonly List<Adventurer> Adventurers = AdventurerList();
    private List<Adventurer> _deadAdventurers = new List<Adventurer>();
    public static readonly string GroupName = nameof(AdventurerManager);

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        base._EnterTree();
        AddToGroup(GroupName);
    }

    public override void _Ready()
    {
        base._Ready();
        _eventBus.OnTimeTick += TimePasses;
        _eventBus.OnStartNewDay += OnStartNewDay;
        _storeManager = (StoreManager)Root.SceneTree.GetFirstNodeInGroup(StoreManager.GroupName);
    }
    private void OnStartNewDay()
    {
        foreach (var adventurer in Adventurers)
        {
            var roll = GD.RandRange(0, 100);
            if (roll > QuestChance) continue;
            adventurer.Quest = Quest.RandomQuest(adventurer.Rank);
            _eventBus.EmitOnAdventurerGoesOnQuest(adventurer, adventurer.Quest);
        }
    }

    private static List<Adventurer> AdventurerList()
    {
        var t = new List<Adventurer>();
        for (int i = 0; i < AdventureCount; i++)
        {
            t.Add(new Adventurer());
        }

        return t;
    }

    private void TimePasses(long timestamp, int minutes)
    {
        foreach (var adventurer in Adventurers)
        {
            SimulateDay(adventurer, minutes);
        }

        AddNewAdventurers();
        
        //Remove all the dead adventurers
        foreach (var adventurer in _deadAdventurers)
        {
            Adventurers.Remove(adventurer);
        }
        _deadAdventurers = new List<Adventurer>();
    }

    private void AddNewAdventurers()
    {
        if (Adventurers.Count < AdventureCount)
        {
           // var baseChance = 5;
            var maxAdventurers = (int)(AdventureCount * 1.3);
            var slotsFree  = maxAdventurers - Adventurers.Count;

            for (int i = 0; i < slotsFree; i++)
            {
                var rand = GD.RandRange(0, 100);
                if (rand > 15)
                {
                    Adventurers.Add(new Adventurer());
                }
            }
        }
    }

    private void SimulateDay(Adventurer adventurer, int minutes)
    {
        var buyChance = 10;
        var willBuy = GD.RandRange(0, 100) < buyChance;
        if (willBuy & adventurer.Quest is null)
        {
            BuyItem(adventurer);
        } else if (adventurer.Quest is not null)
        {
            adventurer.Quest.ProgressTime(adventurer, minutes);
        }
        
        if (adventurer.IsDead)
        {
            _eventBus.EmitOnAdventurerDeath(adventurer);
            _deadAdventurers.Add(adventurer);
        }
    }

    private void BuyItem(Adventurer adventurer)
    {
        var storeItems = _storeManager.Store.items;
        if (storeItems.Count <= 0) return;

        var (randomEquipment, field) = adventurer.Equipment.PickRandomEquipment();
        if (randomEquipment is null)
        {
            var item = GetItemThatAdventurerWants(storeItems, field);
            if (item is not null)
            {
                SetAdventurerFieldToItem(adventurer, item, field);
                _storeManager.Store.BuyItem(_eventBus, item, adventurer);
                DetermineLevelUp(adventurer);
            }
        }

        if (randomEquipment is not null)
        {
            var possibleUpgradeEquipment = GetPossibleUpgradeThatAdventurerWants(storeItems, field, adventurer);
            if (possibleUpgradeEquipment is not null)
            {
                SetAdventurerFieldToItem(adventurer, possibleUpgradeEquipment, field);
                _storeManager.Store.BuyItem(_eventBus, possibleUpgradeEquipment, adventurer);
                DetermineLevelUp(adventurer);
            }
        }
    }

    private void SetAdventurerFieldToItem(Adventurer adventurer,
                                          Item item,
                                          EEquipmentField field)
    {
        switch (field)
        {
            case EEquipmentField.Helmet:
                adventurer.Equipment.Helmet = (Armour)item;
                break;
            case EEquipmentField.ChestPlate:
                adventurer.Equipment.ChestPlate = (Armour)item;
                break;
            case EEquipmentField.Leggings:
                adventurer.Equipment.Leggings = (Armour)item;
                break;
            case EEquipmentField.Boots:
                adventurer.Equipment.Boots = (Armour)item;
                break;
            case EEquipmentField.PrimaryWeapon:
                adventurer.Equipment.PrimaryWeapon = (Weapon)item;
                break;
            case EEquipmentField.Shield:
                adventurer.Equipment.Shield = (Shield)item;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(field), field, null);
        }
        
    }

    private void DetermineLevelUp(Adventurer adventurer)
    {
        Console.WriteLine($"Current adventurer {adventurer.Name} rank: {adventurer.Rank}. Score: {adventurer.GetTotalAdventurerScore()}");

        var score = adventurer.GetTotalAdventurerScore();

        if (adventurer.Rank == ERank.Bronze && score > 20)
        {
            adventurer.Rank = ERank.Silver;
            _eventBus.EmitOnAdventurerLevelUp(ERank.Bronze, ERank.Silver, adventurer);
        }

        if (adventurer.Rank == ERank.Silver && score > 30)
        {
            adventurer.Rank = ERank.Gold;
            _eventBus.EmitOnAdventurerLevelUp(ERank.Silver, ERank.Gold, adventurer);
        }

        if (adventurer.Rank == ERank.Gold && score > 35)
        {
            adventurer.Rank = ERank.Diamond;
            _eventBus.EmitOnAdventurerLevelUp(ERank.Gold, ERank.Diamond, adventurer);
        }

        if (adventurer.Rank == ERank.Diamond && score > 40)
        {
            adventurer.Rank = ERank.Legendary;
            _eventBus.EmitOnAdventurerLevelUp(ERank.Diamond, ERank.Legendary, adventurer);
        }
    }

    private Item? GetPossibleUpgradeThatAdventurerWants(List<Item> storeItems,
                                                        EEquipmentField field,
                                                        Adventurer adventurer)
    {
        return storeItems.FirstOrDefault(x =>
        {
            if (x is Weapon weapon && field == EEquipmentField.PrimaryWeapon && adventurer.Equipment.PrimaryWeapon is not null)
            {
                if ((int)weapon.Rank > (int)adventurer.Equipment.PrimaryWeapon.Rank)
                {
                    if (weapon.Rank is not ERank.Legendary && adventurer.Rank is not ERank.Legendary)
                    {
                        return (int)weapon.Rank == (int)adventurer.Rank + 1;
                    }

                    if (adventurer.Rank is ERank.Legendary && adventurer.Equipment.PrimaryWeapon.Rank is not ERank.Legendary)
                    {
                        return true;
                    }
                }
            }

            if (x is Armour armour)
            {
                if (field == EEquipmentField.Helmet &&
                    adventurer.Equipment.Helmet is not null &&
                    CanUpgradeArmour(armour, adventurer.Equipment.Helmet, adventurer.Rank))
                {
                    return true;
                }

                if (field == EEquipmentField.ChestPlate &&
                    adventurer.Equipment.ChestPlate is not null &&
                    CanUpgradeArmour(armour, adventurer.Equipment.ChestPlate, adventurer.Rank))
                {
                    return true;
                }

                if (field == EEquipmentField.Leggings &&
                    adventurer.Equipment.Leggings is not null &&
                    CanUpgradeArmour(armour, adventurer.Equipment.Leggings, adventurer.Rank))
                {
                    return true;
                }

                if (field == EEquipmentField.Boots &&
                    adventurer.Equipment.Boots is not null &&
                    CanUpgradeArmour(armour, adventurer.Equipment.Boots, adventurer.Rank))
                {
                    return true;
                }
            }

            if (x is Shield shield)
            {
                if (field == EEquipmentField.Shield &&
                    adventurer.Equipment.Shield is not null &&
                    CanUpgradeShield(shield, adventurer.Equipment.Shield, adventurer.Rank))
                {
                    return true;
                }
            }

            return false;
        });
    }

    private bool CanUpgradeShield(Shield shieldFromStore,
                                  Shield shieldFromAdventurer,
                                  ERank adventurerRank)
    {
        if (shieldFromStore.Rank > shieldFromAdventurer.Rank)
        {
            if (shieldFromAdventurer.Rank is not ERank.Legendary && adventurerRank is not ERank.Legendary)
            {
                return (int)shieldFromStore.Rank == (int)adventurerRank + 1;
            }

            if (adventurerRank is ERank.Legendary && shieldFromAdventurer.Rank is not ERank.Legendary)
            {
                return true;
            }
        }

        return false;
    }

    private bool CanUpgradeArmour(Armour armourFromStore,
                                  Armour armourFromAdventurer,
                                  ERank adventurerRank)
    {
        if (armourFromStore.Rank > armourFromAdventurer.Rank)
        {
            if (armourFromAdventurer.Rank is not ERank.Legendary && adventurerRank is not ERank.Legendary)
            {
                return (int)armourFromStore.Rank == (int)adventurerRank + 1;
            }

            if (adventurerRank is ERank.Legendary && armourFromAdventurer.Rank is not ERank.Legendary)
            {
                return true;
            }
        }

        return false;
    }

    private static Item? GetItemThatAdventurerWants(List<Item> storeItems, EEquipmentField field)
    {
        return storeItems.FirstOrDefault(x =>
        {
            if (x is Weapon weapon && field == EEquipmentField.PrimaryWeapon)
            {
                return true;
            }

            if (x is Armour armour)
            {
                return (armour.EquipmentSlot == EArmour.Boots && field == EEquipmentField.Boots) ||
                       (armour.EquipmentSlot == EArmour.Chestplate && field == EEquipmentField.ChestPlate) ||
                       (armour.EquipmentSlot == EArmour.Helmet && field == EEquipmentField.Helmet) ||
                       (armour.EquipmentSlot == EArmour.Leggings && field == EEquipmentField.Leggings);
            }

            if (x is Shield)
            {
                return field == EEquipmentField.Shield;
            }

            return false;
        });
    }
}