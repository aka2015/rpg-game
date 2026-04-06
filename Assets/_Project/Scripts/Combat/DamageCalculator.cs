namespace Project.Combat;

public static class DamageCalculator
{
    public static int Calculate(int attackerAttack, int defenderDefense)
    {
        var finalDamage = attackerAttack - defenderDefense;
        return finalDamage < 1 ? 1 : finalDamage;
    }
}
