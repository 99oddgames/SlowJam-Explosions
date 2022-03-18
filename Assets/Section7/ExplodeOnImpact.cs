using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : GameplayComponent
{
    public Delay Delay;
    public GameObject Explosion;
    public VFX WarningVFX;

    private bool explosionImminent;

    public override void Subscribe(IEventSubscriber eventHandler)
    {
        eventHandler.Subscribe<GameEvents.BetterForceImpact>(OnForceImpact);
    }

    private void Start()
    {
        enabled = false;
    }

    public void OnForceImpact(GameEvents.BetterForceImpact impact)
    {
        if (explosionImminent)
            return;

        explosionImminent = true;
        Delay.Next();
        enabled = true;

        var warningVFX = Instantiate(WarningVFX, transform);
        warningVFX.transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        if (!Delay.IsUp)
            return;

        Destroy(gameObject);
        Instantiate(Explosion, transform.position, Quaternion.identity);
    }
}
