using Godot;
using Project.Core;
using Project.Enemy;
using Project.Quest;
using Project.Save;

namespace Project.World;

public partial class WorldController : Node3D
{
    [Export] public NodePath EnemyPath = "EnemyDummy";
    [Export] public string ActiveQuestId = "kill_001";

    private QuestRuntime _killQuest = default!;

    public override void _Ready()
    {
        _killQuest = new QuestRuntime(ActiveQuestId, "enemy_dummy", 3);
        GameManager.Instance.QuestManager.AddQuest(_killQuest);

        GameManager.Instance.EventBus.Subscribe<EnemyDiedEvent>(OnEnemyDied);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event.IsActionPressed("attack_test"))
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
        if (enemy == null)
        {
            GD.Print("No enemy to attack.");
            return;
        }

        enemy.ApplyDamage(25);
    }

    private void QuickSave()
    {
        var data = new SaveData
        {
            ActiveSceneName = SceneFilePath,
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

        GD.Print($"Loaded quest: {data.Quests[0].QuestId} progress={data.Quests[0].CurrentCount}");
    }
}
