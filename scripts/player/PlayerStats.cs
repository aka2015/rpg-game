namespace Project.Player;

public sealed class PlayerStats
{
    public int MaxHp { get; private set; } = 100;
    public int CurrentHp { get; private set; } = 100;
    public int MaxStamina { get; private set; } = 100;
    public int CurrentStamina { get; private set; } = 100;
    public int Attack { get; private set; } = 10;
    public int Defense { get; private set; } = 4;
    public int Level { get; private set; } = 1;
    public int Experience { get; private set; }
    public int Gold { get; private set; }

    public void ReceiveDamage(int amount)
    {
        CurrentHp -= amount;
        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
    }

    public bool ConsumeStamina(int amount)
    {
        if (CurrentStamina < amount)
        {
            return false;
        }

        CurrentStamina -= amount;
        return true;
    }

    public void AddExperience(int amount)
    {
        Experience += amount;
        while (Experience >= RequiredExperienceForNextLevel())
        {
            Experience -= RequiredExperienceForNextLevel();
            LevelUp();
        }
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        if (Gold < 0)
        {
            Gold = 0;
        }
    }

    public void Restore(int level, int experience, int currentHp, int currentStamina, int gold)
    {
        Level = level < 1 ? 1 : level;
        Experience = experience < 0 ? 0 : experience;
        Gold = gold < 0 ? 0 : gold;

        RecalculateDerivedFromLevel();

        CurrentHp = currentHp;
        if (CurrentHp < 0)
        {
            CurrentHp = 0;
        }
        if (CurrentHp > MaxHp)
        {
            CurrentHp = MaxHp;
        }

        CurrentStamina = currentStamina;
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
    }

    private int RequiredExperienceForNextLevel()
    {
        return Level * 100;
    }

    private void LevelUp()
    {
        Level += 1;
        MaxHp += 10;
        MaxStamina += 5;
        Attack += 2;
        Defense += 1;
        CurrentHp = MaxHp;
        CurrentStamina = MaxStamina;
    }

    private void RecalculateDerivedFromLevel()
    {
        MaxHp = 100 + ((Level - 1) * 10);
        MaxStamina = 100 + ((Level - 1) * 5);
        Attack = 10 + ((Level - 1) * 2);
        Defense = 4 + (Level - 1);
    }
}
