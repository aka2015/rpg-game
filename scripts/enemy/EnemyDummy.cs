using Godot;
using Project.Core;

namespace Project.Enemy;

public partial class EnemyDummy : Node3D
{
    [Export] public string EnemyId = "enemy_dummy";
    [Export] public int MaxHp = 50;
    [Export] public float HitFeedbackScale = 1.08f;

    private EnemyStats _stats = default!;

    public override void _Ready()
    {
        _stats = new EnemyStats(EnemyId, MaxHp, 8, 2, 15);
        AddToGroup("enemy_dummy");
    }

    public void ApplyDamage(int incoming)
    {
        var isDead = _stats.ApplyDamage(incoming);
        PlayHitFeedback();

        if (GameManager.Instance != null)
        {
            var message = isDead ? "Enemy defeated" : $"Enemy hit ({_stats.CurrentHp} HP left)";
            GameManager.Instance.EventBus.Publish(new CombatFeedbackEvent(message));
        }

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

    private void PlayHitFeedback()
    {
        var originalScale = Scale;
        var tween = CreateTween();
        tween.TweenProperty(this, "scale", originalScale * HitFeedbackScale, 0.06f);
        tween.TweenProperty(this, "scale", originalScale, 0.08f);
    }
}
