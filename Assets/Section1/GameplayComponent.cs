using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameplayComponent : MonoBehaviour
{
    public virtual void Subscribe(IEventSubscriber eventHandler) { }
}
