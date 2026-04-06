using Godot;

namespace Project.Core;

public partial class GameManager : Node
{
    public static GameManager Instance { get; private set; } = default!;

    public IEventBus EventBus { get; } = new EventBus();

    public override void _EnterTree()
    {
        if (Instance != null && Instance != this)
        {
            QueueFree();
            return;
        }

        Instance = this;
    }
}
