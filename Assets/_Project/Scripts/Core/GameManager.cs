namespace Project.Core;

public sealed class GameManager
{
    public IEventBus EventBus { get; }

    public GameManager(IEventBus eventBus)
    {
        EventBus = eventBus;
    }
}
