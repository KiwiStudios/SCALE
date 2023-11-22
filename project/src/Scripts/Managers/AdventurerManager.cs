using System.Collections.Generic;
using System.Linq;
using SCALE.Enums;

namespace SCALE.Scripts.Managers;

public partial class AdventurerManager : Node
{
    private EventBus _eventBus = null!;
    private const int AdventureCount = 50;
    private StoreManager _storeManager = null!;

    private List<Adventurer> _adventurers = AdventurerList();

    public override void _EnterTree()
    {
        _eventBus = this.GetEventBus();
        base._EnterTree();
    }

    public override void _Ready()
    {
        base._Ready();
        _eventBus.OnTimeTick += AdventureBuying;
        _storeManager = (StoreManager)Root.SceneTree.GetFirstNodeInGroup(StoreManager.GroupName);
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

    private void AdventureBuying(long timestamp)
    {
        foreach (var adventurer in _adventurers)
        {
            SimulateDay(adventurer);
        }
    }

    private void SimulateDay(Adventurer adventurer)
    {
        var buyChance = 1;
        var willBuy = GD.RandRange(0, 100) < buyChance;
        if (willBuy)
        {
            BuyItem(adventurer);
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
                var success = _storeManager.Store.BuyItem(_eventBus, item, adventurer);
                if (success) SetAdventurerFieldToItem(adventurer, item, field);
            }
        }

        if (randomEquipment is not null)
        {
            var possibleUpgradeEquipment = GetPossibleUpgradeThatAdventurerWants(storeItems, field, adventurer);
            if (possibleUpgradeEquipment is not null)
            {
                var success = _storeManager.Store.BuyItem(_eventBus, possibleUpgradeEquipment, adventurer);
                if (success) SetAdventurerFieldToItem(adventurer, possibleUpgradeEquipment, field);
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