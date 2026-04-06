using Godot;
using Project.Core;

namespace Project.UI;

public partial class HudController : CanvasLayer
{
    [Export] public Label QuestLabel = default!;
    [Export] public Label StatusLabel = default!;
    [Export] public Label StatsLabel = default!;

    public override void _Ready()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.EventBus.Subscribe<QuestProgressChangedEvent>(OnQuestProgressChanged);
        GameManager.Instance.EventBus.Subscribe<QuestCompletedEvent>(OnQuestCompleted);
        GameManager.Instance.EventBus.Subscribe<QuestTurnedInEvent>(OnQuestTurnedIn);
        GameManager.Instance.EventBus.Subscribe<PlayerStatsChangedEvent>(OnPlayerStatsChanged);
        GameManager.Instance.EventBus.Subscribe<SaveOperationEvent>(OnSaveOperation);
        GameManager.Instance.EventBus.Subscribe<CombatFeedbackEvent>(OnCombatFeedback);
    }

    private void OnQuestProgressChanged(QuestProgressChangedEvent gameEvent)
    {
        if (QuestLabel != null)
        {
            QuestLabel.Text = $"Quest {gameEvent.QuestId}: {gameEvent.Current}/{gameEvent.Required}";
        }
    }

    private void OnQuestCompleted(QuestCompletedEvent gameEvent)
    {
        if (StatusLabel != null)
        {
            StatusLabel.Text = $"Quest selesai: {gameEvent.QuestId}. Turn-in ke NPC.";
        }
    }

    private void OnQuestTurnedIn(QuestTurnedInEvent gameEvent)
    {
        if (StatusLabel != null)
        {
            StatusLabel.Text = $"Quest turn-in berhasil: {gameEvent.QuestId}";
        }
    }

    private void OnPlayerStatsChanged(PlayerStatsChangedEvent gameEvent)
    {
        if (StatsLabel != null)
        {
            StatsLabel.Text = $"HP {gameEvent.CurrentHp}/{gameEvent.MaxHp} | STA {gameEvent.CurrentStamina}/{gameEvent.MaxStamina} | LV {gameEvent.Level} | Gold {gameEvent.Gold}";
        }
    }

    private void OnSaveOperation(SaveOperationEvent gameEvent)
    {
        if (StatusLabel != null)
        {
            StatusLabel.Text = gameEvent.Message;
        }
    }

    private void OnCombatFeedback(CombatFeedbackEvent gameEvent)
    {
        if (StatusLabel != null)
        {
            StatusLabel.Text = gameEvent.Message;
        }
    }
}
