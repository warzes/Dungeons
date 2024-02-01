using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public const int pointsByLevel = 5;
    public const int maxSkillLevel = 10;

    public Dictionary<int, int> levels;

    public int Level { get; private set; }
    public int Points { get; private set; }
    public int Experience { get; private set; }
    public int MaxLevel { get; private set; }

    public int CurrentLevelExperience
    {
        get { return levels[Level]; }
    }

    public int NextLevelExperience
    {
        get { return Level >= MaxLevel ? levels[Level] : levels[Level + 1]; }
    }

    // Character
    public int Strenght { get; private set; }
    public int Stamina { get; private set; }
    public int Mind { get; private set; }
    public int Willpower { get; private set; }

    // Items
    public int AdditionalStrenght { get; private set; }
    public int AdditionalStamina { get; private set; }
    public int AdditionalMind { get; private set; }
    public int AdditionalWillpower { get; private set; }

    public int ActualStrenght { get { return Strenght + AdditionalStrenght; } }
    public int ActualStamina { get { return Stamina + AdditionalStamina; } }
    public int ActualMind { get { return Mind + AdditionalMind; } }
    public int ActualWillpower { get { return Willpower + AdditionalWillpower; } }

    void Awake()
    {
        levels = new Dictionary<int, int>();
        Level = 1;
        MaxLevel = 8;
        levels.Add(1, 0);
        levels.Add(2, 300);
        levels.Add(3, 800);
        levels.Add(4, 1500);
        levels.Add(5, 2500);
        levels.Add(6, 4000);
        levels.Add(7, 6000);
        levels.Add(8, 10000);
    }

    public void AddStrenght(int points, bool fromItem)
    {
        if (fromItem == false)
        {
            Strenght += points;
            AddPoints(-points);
        }
        else
        {
            AdditionalStrenght += points;
        }
    }

    public void AddStamina(int points, bool fromItem)
    {
        if (fromItem == false)
        {
            Stamina += points;
            AddPoints(-points);
        }
        else
        {
            AdditionalStamina += points;
        }
        PlayerController.Instance.MaxHealth = PlayerController.Instance.startMaxHealth + (10 * ActualStamina);
    }

    public void AddMind(int points, bool fromItem)
    {
        if (fromItem == false)
        {
            Mind += points;
            AddPoints(-points);
        }
        else
        {
            AdditionalMind += points;
        }
    }

    public void AddWillpower(int points, bool fromItem)
    {
        if (fromItem == false)
        {
            Willpower += points;
            AddPoints(-points);
        }
        else
        {
            AdditionalWillpower += points;
        }
        PlayerController.Instance.MaxMana = PlayerController.Instance.startMaxMana + (10 * ActualWillpower);
    }

    public void AddExperience(int exp)
    {
        Experience += exp;
        int newLevel = 0;
        foreach (KeyValuePair<int, int> pair in levels)
        {
            if (Experience >= pair.Value) newLevel = pair.Key;
        }
        AddPoints((newLevel - Level) * pointsByLevel);
        Level = newLevel;
    }

    public void AddPoints(int points)
    {
        Points += points;
    }
}

