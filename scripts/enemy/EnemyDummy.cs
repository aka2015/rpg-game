using Godot;
using Project.Core;

namespace Project.Enemy;

public partial class EnemyDummy : Node3D
{
    [Export] public string EnemyId = "enemy_dummy";
    [Export] public int MaxHp = 50;

    private EnemyStats _stats = default!;

    public override void _Ready()
    {
        _stats = new EnemyStats(EnemyId, MaxHp, 8, 2, 15);
        AddToGroup("enemy_dummy");
    }

    public void ApplyDamage(int incoming)
    {
        var isDead = _stats.ApplyDamage(incoming);
        if (!isDead)
        {
            return;
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.EventBus.Publish(new EnemyDiedEvent(EnemyId));
        }

        QueueFree();
    }
}
