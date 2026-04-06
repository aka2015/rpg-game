using System;
using System.Collections.Generic;

namespace Project.Core;

public sealed class EventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _handlers = new();

    public void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
    {
        var type = typeof(TEvent);
        if (!_handlers.TryGetValue(type, out var delegates))
        {
            delegates = new List<Delegate>();
            _handlers[type] = delegates;
        }

        if (!delegates.Contains(handler))
        {
            delegates.Add(handler);
        }
    }

    public void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
    {
        var type = typeof(TEvent);
        if (_handlers.TryGetValue(type, out var delegates))
        {
            delegates.Remove(handler);
            if (delegates.Count == 0)
            {
                _handlers.Remove(type);
            }
        }
    }

    public void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent
    {
        var type = typeof(TEvent);
        if (!_handlers.TryGetValue(type, out var delegates))
        {
            return;
        }

        var snapshot = delegates.ToArray();
        foreach (var handler in snapshot)
        {
            ((Action<TEvent>)handler).Invoke(gameEvent);
        }
    }
}
