using Godot;
using Project.Core;

namespace Project.UI;

public partial class HudController : CanvasLayer
{
    [Export] public Label QuestLabel = default!;

    public override void _Ready()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.EventBus.Subscribe<QuestProgressChangedEvent>(OnQuestProgressChanged);
    }

    private void OnQuestProgressChanged(QuestProgressChangedEvent gameEvent)
    {
        if (QuestLabel != null)
        {
            QuestLabel.Text = $"Quest {gameEvent.QuestId}: {gameEvent.Current}/{gameEvent.Required}";
        }
    }
}
