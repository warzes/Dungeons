[System.Serializable]
public class ItemPotion : Item
{
    public enum PotionTypes
    {
        Health = 0,
        Mana = 1,
        Experience = 2
    }

    public PotionTypes potionType;
    public int healthPoints;
    public int manaPoints;
    public int experiencePoints;

    public ItemPotion()
    {
        itemType = ItemTypes.Potion;
    }

    public override void Activate()
    {
        SoundController.Play("drink");
        if (potionType == PotionTypes.Health)
        {
            PlayerController.Instance.Health += healthPoints;
        }
        if (potionType == PotionTypes.Mana)
        {
            PlayerController.Instance.Mana += manaPoints;
        }
        if (potionType == PotionTypes.Experience)
        {
            PlayerController.Instance.experience.AddExperience(experiencePoints);
        }
    }
}

