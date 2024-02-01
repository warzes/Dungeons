[System.Serializable]
public class ItemWeaponMelee : ItemWeapon
{
    public enum WeaponMeleeTypes
    {
        Sword = 0,
        Axe = 1,
    }

    public DamageConfig damage;
    public WeaponMeleeTypes weaponMeleeType;

    public ItemWeaponMelee() :
        base()
    {
        itemType = ItemTypes.WeaponMelee;
        weaponMeleeType = WeaponMeleeTypes.Sword;
        damage = new DamageConfig();
    }
}

