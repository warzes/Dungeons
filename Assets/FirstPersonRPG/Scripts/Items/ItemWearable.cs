[System.Serializable]
public class ItemWearable : Item
{
    public enum WearableTypes
    {
        Armor = 0,
        Helmet = 1,
        Amulet = 2,
        Ring = 3
    }

    public WearableTypes wearableType;

    public DefenceConfig defenceBonus;
    public int strenghtBonus;
    public int staminaBonus;
    public int mindBonus;
    public int willpowerBonus;

    public ItemWearable()
    {
        itemType = ItemTypes.Wearable;
        defenceBonus = new DefenceConfig();
    }

    public override void Activate()
    {
        base.Activate();
        PlayerController.Instance.inventory.SetWearable(this);
        SoundController.Play("equip");

        ExperienceController controller = PlayerController.Instance.experience;
        controller.AddStrenght(strenghtBonus, true);
        controller.AddStamina(staminaBonus, true);
        controller.AddMind(mindBonus, true);
        controller.AddWillpower(willpowerBonus, true);

        DefenceConfig defence = PlayerController.Instance.defence;
        defence.physical += defenceBonus.physical;
        defence.fire += defenceBonus.fire;
        defence.ice += defenceBonus.ice;
        defenceBonus.electro += defenceBonus.electro;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        PlayerController.Instance.inventory.AddItem(this);

        ExperienceController controller = PlayerController.Instance.experience;
        controller.AddStrenght(-strenghtBonus, true);
        controller.AddStamina(-staminaBonus, true);
        controller.AddMind(-mindBonus, true);
        controller.AddWillpower(-willpowerBonus, true);

        DefenceConfig defence = PlayerController.Instance.defence;
        defence.physical -= defenceBonus.physical;
        defence.fire -= defenceBonus.fire;
        defence.ice -= defenceBonus.ice;
        defenceBonus.electro -= defenceBonus.electro;
    }
}

