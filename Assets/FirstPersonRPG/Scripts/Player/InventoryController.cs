using System.Collections.Generic;
using UnityEngine;

public class InventoryController : Singleton<InventoryController>
{
    public event System.Action<ItemWeapon> onSetWeapon;
    public event System.Action<ItemWearable> onSetWearable;

    private List<Item> items;

    public int slotsCount = 23;
    public List<Item> Items { get { return items; } }
    public ItemWeapon Weapon { get; private set; }
    public ItemWearable Armor { get; private set; }
    public ItemWearable Helmet { get; private set; }
    public ItemWearable Amulet { get; private set; }
    public ItemWearable Ring { get; private set; }

    void Awake()
    {
        items = new List<Item>(new Item[slotsCount]);
    }

    void Update()
    {
        if (GameController.Instance.GameState == GameStates.Game)
        {
            if (InputController.Item1) UseItem(0);
            if (InputController.Item2) UseItem(1);
            if (InputController.Item3) UseItem(2);
            if (InputController.Item4) UseItem(3);
            if (InputController.Item5) UseItem(4);
            if (InputController.Item6) UseItem(5);
        }
    }

    public bool AddItem(Item item)
    {
        int index = items.FindIndex(i => i == null);
        if (index == -1) return false;
        items[index] = item;
        return true;
    }

    public void SetWeapon(ItemWeapon weapon)
    {
        if (Weapon != null) Weapon.Deactivate();
        Weapon = weapon;
        if (onSetWeapon != null) onSetWeapon(weapon);
    }

    public void SetArmor(ItemWearable item)
    {
        if (item != null && item.wearableType != ItemWearable.WearableTypes.Armor) return;
        if (Armor != null) Armor.Deactivate();
        Armor = item;
        if (onSetWearable != null) onSetWearable(item);
    }

    public void SetHelmet(ItemWearable item)
    {
        if (item != null && item.wearableType != ItemWearable.WearableTypes.Helmet) return;
        if (Helmet != null) Helmet.Deactivate();
        Helmet = item;
        if (onSetWearable != null) onSetWearable(item);
    }

    public void SetAmulet(ItemWearable item)
    {
        if (item != null && item.wearableType != ItemWearable.WearableTypes.Amulet) return;
        if (Amulet != null) Amulet.Deactivate();
        Amulet = item;
        if (onSetWearable != null) onSetWearable(item);
    }

    public void SetRing(ItemWearable item)
    {
        if (item != null && item.wearableType != ItemWearable.WearableTypes.Ring) return;
        if (Ring != null) Ring.Deactivate();
        Ring = item;
        if (onSetWearable != null) onSetWearable(item);
    }

    public void SetWearable(ItemWearable item)
    {
        if (item.wearableType == ItemWearable.WearableTypes.Armor) SetArmor(item);
        if (item.wearableType == ItemWearable.WearableTypes.Helmet) SetHelmet(item);
        if (item.wearableType == ItemWearable.WearableTypes.Amulet) SetAmulet(item);
        if (item.wearableType == ItemWearable.WearableTypes.Ring) SetRing(item);
    }

    public void UseItem(int slotId)
    {
        if (slotId < 0 || slotId > items.Count - 1) return;
        Item item = items[slotId];
        if (item == null || item.itemType == Item.ItemTypes.Quest) return;
        items[slotId] = null;
        item.Activate();
    }

    public bool FindAndUseItem(string itemId)
    {
        int index = Items.FindIndex(i => i != null && i.id == itemId);
        if (index >= 0)
        {
            Items[index] = null;
            return true;
        }
        return false;
    }

    public void Move(int firstSlot, int secondSlot)
    {
        if (firstSlot >= Items.Count || secondSlot >= Items.Count || firstSlot < 0 || secondSlot < 0) return;
        Item item = items[firstSlot];
        items[firstSlot] = items[secondSlot];
        items[secondSlot] = item;
    }

    public void Drop(int slotId)
    {
        if (slotId >= Items.Count || slotId < 0) return;
        Item item = items[slotId];
        PlayerController.Instance.DropItem(item.id);
        items[slotId] = null;
    }
}

