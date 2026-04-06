using Godot;
using Project.Core;
using Project.Enemy;
using Project.Player;
using Project.Quest;
using Project.Save;

namespace Project.World;

public partial class WorldController : Node3D
{
    private const int DefaultSaveSlot = 1;

    [Export] public NodePath EnemyPath = "EnemyDummy";
    [Export] public NodePath PlayerPath = "Player";
    [Export] public string ActiveQuestId = "kill_001";
    [Export] public float AttackRange = 2.25f;

    private QuestRuntime _killQuest = default!;

    public override void _Ready()
    {
        _killQuest = new QuestRuntime(ActiveQuestId, "enemy_dummy", 3);
        GameManager.Instance.QuestManager.AddQuest(_killQuest);

        GameManager.Instance.EventBus.Subscribe<EnemyDiedEvent>(OnEnemyDied);
        PublishPlayerStats();
    }

    public override void _ExitTree()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.EventBus.Unsubscribe<EnemyDiedEvent>(OnEnemyDied);
        }
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("attack"))
        {
            TryAttackEnemy();
        }

        if (@event.IsActionPressed("quick_save"))
        {
            QuickSave();
        }

        if (@event.IsActionPressed("quick_load"))
        {
            QuickLoad();
        }
    }

    public void StartQuestFromNpc()
    {
        GameManager.Instance.QuestManager.StartQuest(ActiveQuestId);
    }

    private void OnEnemyDied(EnemyDiedEvent gameEvent)
    {
        GameManager.Instance.QuestManager.RegisterEnemyDeath(gameEvent.EnemyId);

        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (player != null)
        {
            player.Stats.AddExperience(15);
            PublishPlayerStats();
        }
    }

    private void TryAttackEnemy()
    {
        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (player == null)
        {
            GD.Print("No player found for attack.");
            return;
        }

        var from = player.GlobalPosition + Vector3.Up;
        var forward = -player.GlobalBasis.Z;
        var to = from + forward * AttackRange;

        var query = PhysicsRayQueryParameters3D.Create(from, to);
        query.CollideWithAreas = true;
        var result = GetWorld3D().DirectSpaceState.IntersectRay(query);

        if (result.Count == 0)
        {
            GameManager.Instance.EventBus.Publish(new SaveOperationEvent("Attack missed", false));
            return;
        }

        var collider = result["collider"].AsGodotObject() as Node;
        var enemy = FindEnemyAncestor(collider);
        if (enemy == null)
        {
            GameManager.Instance.EventBus.Publish(new SaveOperationEvent("Hit non-enemy target", false));
            return;
        }

        enemy.ApplyDamage(25);
    }

    private static EnemyDummy? FindEnemyAncestor(Node? collider)
    {
        var current = collider;
        while (current != null)
        {
            if (current is EnemyDummy enemy)
            {
                return enemy;
            }

            current = current.GetParent();
        }

        return null;
    }

    private void QuickSave()
    {
        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        var playerPos = player?.GlobalPosition ?? Vector3.Zero;
        var stats = player?.Stats;

        var data = new SaveData
        {
            ActiveSceneName = SceneFilePath,
            PlayerPosX = playerPos.X,
            PlayerPosY = playerPos.Y,
            PlayerPosZ = playerPos.Z,
            PlayerLevel = stats?.Level ?? 1,
            PlayerExperience = stats?.Experience ?? 0,
            PlayerCurrentHp = stats?.CurrentHp ?? 100,
            PlayerCurrentStamina = stats?.CurrentStamina ?? 100,
            Quests = new[]
            {
                new QuestSnapshot
                {
                    QuestId = _killQuest.QuestId,
                    CurrentCount = _killQuest.CurrentCount,
                    Status = _killQuest.Status
                }
            }
        };

        GameManager.Instance.SaveManager.SaveToSlot(DefaultSaveSlot, data);
        GameManager.Instance.EventBus.Publish(new SaveOperationEvent($"Saved slot {DefaultSaveSlot}", true));
    }

    private void QuickLoad()
    {
        var data = GameManager.Instance.SaveManager.LoadFromSlot(DefaultSaveSlot);
        if (data.Quests.Length == 0)
        {
            GameManager.Instance.EventBus.Publish(new SaveOperationEvent($"Slot {DefaultSaveSlot} empty", false));
            return;
        }

        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (player != null)
        {
            player.GlobalPosition = new Vector3(data.PlayerPosX, data.PlayerPosY, data.PlayerPosZ);
            player.RestoreStats(data.PlayerLevel, data.PlayerExperience, data.PlayerCurrentHp, data.PlayerCurrentStamina);
        }

        var quest = data.Quests[0];
        GameManager.Instance.QuestManager.RestoreQuest(quest.QuestId, quest.CurrentCount, quest.Status);
        PublishPlayerStats();
        GameManager.Instance.EventBus.Publish(new SaveOperationEvent($"Loaded slot {DefaultSaveSlot}", true));
    }

    private void PublishPlayerStats()
    {
        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (player == null)
        {
            return;
        }

        var stats = player.Stats;
        GameManager.Instance.EventBus.Publish(
            new PlayerStatsChangedEvent(stats.CurrentHp, stats.MaxHp, stats.CurrentStamina, stats.MaxStamina, stats.Level));
    }
}
