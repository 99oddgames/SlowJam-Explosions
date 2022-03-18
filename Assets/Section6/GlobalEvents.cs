using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEvents : IEventRaiser
{
    private static GlobalEvents instance;
    private IEventRaiser eventRaiser;

    public GlobalEvents (IEventRaiser eventRaiser)
    {
        this.eventRaiser = eventRaiser;
        instance = this;
    }

    public static void SendMessage<T>(T message)
    {
        instance.Raise(message);
    }

    public void Raise<T>(T message)
    {
        eventRaiser.Raise(message);
    }

    public void Dispose()
    {
        instance = null;
    }
}
