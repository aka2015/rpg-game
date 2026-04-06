using System;

namespace Project.Core;

public interface IEventBus
{
    void Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent;
    void Unsubscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent;
    void Publish<TEvent>(TEvent gameEvent) where TEvent : IGameEvent;
}
