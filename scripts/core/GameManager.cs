using Godot;
using Project.Quest;
using Project.Save;

namespace Project.Core;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; } = default!;

    public IEventBus EventBus { get; } = new EventBus();
    public QuestManager QuestManager { get; private set; } = default!;
    public SaveManager SaveManager { get; private set; } = default!;

    public override void _EnterTree()
    {
        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }

        Instance = this;
    }

    public override void _Ready()
    {
        QuestManager = new QuestManager(EventBus);
        SaveManager = new SaveManager();
    }
}
