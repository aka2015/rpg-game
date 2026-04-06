using Godot;
using Project.Core;

namespace Project.UI;

public partial class HudController : CanvasLayer
{
    [Export] public Label QuestLabel = default!;
    [Export] public Label StatusLabel = default!;

    public override void _Ready()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.EventBus.Subscribe<QuestProgressChangedEvent>(OnQuestProgressChanged);
        GameManager.Instance.EventBus.Subscribe<QuestCompletedEvent>(OnQuestCompleted);
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
            StatusLabel.Text = $"Quest selesai: {gameEvent.QuestId}";
        }
    }
}
