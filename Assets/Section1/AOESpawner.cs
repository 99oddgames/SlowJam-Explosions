using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOESpawner : GameplayComponent
{
    public GameObject Prefab;

    public override void Subscribe(IEventSubscriber eventHandler)
    {
        eventHandler.Subscribe<GameEvents.MouseClick>(OnMouseClick);
    }

    private void OnMouseClick(GameEvents.MouseClick clickInfo)
    {
        Instantiate(Prefab, clickInfo.WorldPosition, Quaternion.identity);
    }
}
