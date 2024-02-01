using UnityEngine;

[System.Serializable]
public class ItemWeaponStaff : ItemWeapon
{
    public GameObject bullet;
    public int mana;

    public ItemWeaponStaff():
        base()
    {
        itemType = ItemTypes.Staff;
    }
}

