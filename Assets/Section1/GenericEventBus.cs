using System;
using System.Collections.Generic;

//<summary>Event bus that stores and dispatches events based on message type alone</summary>
public class GenericEventBus : IEventHandler
{
    private Dictionary<Type, EventHandler> handlers = new Dictionary<Type, EventHandler>(100);

    public void Subscribe<T>(Action<T> action)
    {
        var type = typeof(T);

        if(handlers.TryGetValue(type, out var handler))
        {
            var concreteHandler = (ConcreteEventHandler<T>)handler;
            concreteHandler.Event += action;
        }
        else
        {
            handlers.Add(type, new ConcreteEventHandler<T>(action));
        }
    }

    public void Raise<T>(T message)
    {
        var type = typeof(T);

        if (!handlers.TryGetValue(type, out var handler))
            return;

        var concreteHandler = (ConcreteEventHandler<T>)handler;
        concreteHandler.Event(message);
    }

    public void Clear()
    {
        handlers.Clear();
    }
}

//<summary>Subscription contract, expose event handlers with this to potential subscribers</summary>
public interface IEventSubscriber
{
    void Subscribe<T>(Action<T> action);
}

//<summary>Raise contract, expose event handlers with this to potential raisers</summary>
public interface IEventRaiser
{
    void Raise<T>(T message);
}

public interface IEventHandler : IEventRaiser, IEventSubscriber 
{
    void Clear();
}

//<summary>Wraps event handler lambdas for storage and shared logic</summary>
public class EventHandler { }

public class ConcreteEventHandler<T> : EventHandler
{
    public Action<T> Event;

    public ConcreteEventHandler(Action<T> action)
    {
        Event = action;
    }
}
