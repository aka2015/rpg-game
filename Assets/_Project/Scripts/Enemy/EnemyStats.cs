namespace Project.Enemy;

public sealed class EnemyStats
{
    public string EnemyId { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; private set; }
    public int Attack { get; }
    public int Defense { get; }
    public int ExperienceReward { get; }

    public EnemyStats(string enemyId, int maxHp, int attack, int defense, int experienceReward)
    {
        EnemyId = enemyId;
        MaxHp = maxHp;
        CurrentHp = maxHp;
        Attack = attack;
        Defense = defense;
        ExperienceReward = experienceReward;
    }

    public bool ApplyDamage(int amount)
    {
        CurrentHp -= amount;
        if (CurrentHp <= 0)
        {
            CurrentHp = 0;
            return true;
        }

        return false;
    }
}
