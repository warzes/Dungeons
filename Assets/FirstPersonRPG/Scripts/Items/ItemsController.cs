using System.Collections.Generic;

[System.Serializable]
public class ItemsController : Singleton<ItemsController>
{
    public List<ItemQuest> things = new List<ItemQuest>();
    public List<ItemWeaponMelee> weapons = new List<ItemWeaponMelee>();
    public List<ItemWeaponStaff> staffs = new List<ItemWeaponStaff>();
    public List<ItemWearable> wearable = new List<ItemWearable>();
    public List<ItemPotion> potions = new List<ItemPotion>();

    public List<Item> Items
    {
        get
        {
            List<Item> list = new List<Item>();
            list.AddRange(things.ToArray());
            list.AddRange(weapons.ToArray());
            list.AddRange(staffs.ToArray());
            list.AddRange(wearable.ToArray());
            list.AddRange(potions.ToArray());
            return list;
        }
    }

    public Item GetItemById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        Item item = Items.Find(i => i.id == id);
        return item;
    }

    public void AddItem(Item.ItemTypes type)
    {
        if (type == Item.ItemTypes.Quest) things.Add(new ItemQuest());
        if (type == Item.ItemTypes.Potion) potions.Add(new ItemPotion());
        if (type == Item.ItemTypes.Staff) staffs.Add(new ItemWeaponStaff());
        if (type == Item.ItemTypes.WeaponMelee) weapons.Add(new ItemWeaponMelee());
        if (type == Item.ItemTypes.Wearable) wearable.Add(new ItemWearable());
    }

    public void RemoveItem(Item item)
    {
        if (item.itemType == Item.ItemTypes.Quest) things.Remove((ItemQuest)item);
        if (item.itemType == Item.ItemTypes.Potion) potions.Remove((ItemPotion)item);
        if (item.itemType == Item.ItemTypes.Staff) staffs.Remove((ItemWeaponStaff)item);
        if (item.itemType == Item.ItemTypes.WeaponMelee) weapons.Remove((ItemWeaponMelee)item);
        if (item.itemType == Item.ItemTypes.Wearable) wearable.Remove((ItemWearable)item);
    }
}
