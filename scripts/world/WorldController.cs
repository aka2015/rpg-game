using Godot;
using Project.Core;
using Project.Enemy;
using Project.Player;
using Project.Quest;
using Project.Save;

namespace Project.World;

public partial class WorldController : Node3D
{
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
    }

    private void TryAttackEnemy()
    {
        var enemy = GetNodeOrNull<EnemyDummy>(EnemyPath);
        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (enemy == null || player == null)
        {
            GD.Print("No enemy or player to process attack.");
            return;
        }

        var distance = player.GlobalPosition.DistanceTo(enemy.GlobalPosition);
        if (distance > AttackRange)
        {
            GD.Print($"Out of range. Current distance: {distance:0.00}");
            return;
        }

        enemy.ApplyDamage(25);
    }

    private void QuickSave()
    {
        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        var playerPos = player?.GlobalPosition ?? Vector3.Zero;

        var data = new SaveData
        {
            ActiveSceneName = SceneFilePath,
            PlayerPosX = playerPos.X,
            PlayerPosY = playerPos.Y,
            PlayerPosZ = playerPos.Z,
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

        GameManager.Instance.SaveManager.SaveToUserPath("save_01.json", data);
        GD.Print("Saved: user://save_01.json");
    }

    private void QuickLoad()
    {
        var data = GameManager.Instance.SaveManager.LoadFromUserPath("save_01.json");
        if (data.Quests.Length == 0)
        {
            GD.Print("No quest progress found in save.");
            return;
        }

        var player = GetNodeOrNull<PlayerController>(PlayerPath);
        if (player != null)
        {
            player.GlobalPosition = new Vector3(data.PlayerPosX, data.PlayerPosY, data.PlayerPosZ);
        }

        var quest = data.Quests[0];
        GameManager.Instance.QuestManager.RestoreQuest(quest.QuestId, quest.CurrentCount, quest.Status);
        GD.Print($"Loaded quest: {quest.QuestId} progress={quest.CurrentCount}");
    }
}
