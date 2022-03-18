using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObject : MonoBehaviour
{
    private IEventHandler eventHandler = new GenericEventBus();

    private void Awake()
    {
        var components = GetComponentsInChildren<GameplayComponent>(true);

        for(int i = 0; i < components.Length; i++)
        {
            components[i].Subscribe(eventHandler);
        }

        OnAwake(eventHandler);
    }

    protected virtual void OnAwake(IEventHandler eventHandler) { }

    public void SendMessage<T>(T message)
    {
        eventHandler.Raise(message);
    }

    public void Despawn()
    {
        Destroy(gameObject);
    }
}
