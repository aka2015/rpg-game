namespace Project.UI;

public sealed class HudState
{
    public int CurrentHp { get; private set; }
    public int MaxHp { get; private set; }
    public int CurrentStamina { get; private set; }
    public int MaxStamina { get; private set; }
    public int Level { get; private set; }

    public void Sync(int currentHp, int maxHp, int currentStamina, int maxStamina, int level)
    {
        CurrentHp = currentHp;
        MaxHp = maxHp;
        CurrentStamina = currentStamina;
        MaxStamina = maxStamina;
        Level = level;
    }
}
