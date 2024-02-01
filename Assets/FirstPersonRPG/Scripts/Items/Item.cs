using UnityEngine;

[System.Serializable]
public class Item
{
    public enum ItemTypes
    {
        Quest = 0, // Quest items
        Potion = 1,
        WeaponMelee = 2,
        Staff = 3,
        Wearable = 4
    }

    public string id;
    public string name;
    public string description;
    public Sprite icon;
    public ItemTypes itemType;

    public virtual void Activate() { }
    public virtual void Deactivate() { }

    public Item() { }

    public override string ToString()
    {
        return name;
    }
}
