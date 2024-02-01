using UnityEngine;

[System.Serializable]
public class ItemWeapon : Item
{
    public GameObject model;

    public ItemWeapon()
    {
    }

    public override void Activate()
    {
        base.Activate();
        SoundController.Play("equip");
        PlayerController.Instance.inventory.SetWeapon(this);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        PlayerController.Instance.inventory.AddItem(this);
    }

    public static int GetAnimationId(ItemWeapon item)
    {
        if (item.itemType == ItemTypes.Staff) return 0;
        if (item.itemType == ItemTypes.WeaponMelee)
        {
            ItemWeaponMelee weapon = (ItemWeaponMelee)item;
            if (weapon.weaponMeleeType == ItemWeaponMelee.WeaponMeleeTypes.Sword) return 1;
            if (weapon.weaponMeleeType == ItemWeaponMelee.WeaponMeleeTypes.Axe) return 2;
        }
        return 0;
    }
}
