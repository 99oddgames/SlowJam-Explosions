using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : GameplayComponent
{
    private Rigidbody body;

    public override void Subscribe(IEventSubscriber eventHandler)
    {
        eventHandler.Subscribe<GameEvents.ForceImpact>(OnForceImpact);
        eventHandler.Subscribe<GameEvents.BetterForceImpact>(OnForceImpact);
    }

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    public void OnForceImpact(GameEvents.ForceImpact impact)
    {
        var impulse = impact.DirectionNormalized * impact.Force;
        body.AddForce(impulse, ForceMode.Impulse);
    }

    public void OnForceImpact(GameEvents.BetterForceImpact impact)
    {
        if(impact.ResetVelocity)
        {
            body.velocity = Vector3.zero;
        }

        var direction = Vector3.Slerp(impact.DirectionNormalized, Vector3.up, impact.UpwardBias01);
        direction.x *= impact.DirectionalStrength.x;
        direction.y *= impact.DirectionalStrength.y;
        direction.z *= impact.DirectionalStrength.z;

        if(impact.InvertDirection)
        {
            direction = -direction;
        }

        var impulse = direction * impact.Force * impact.Falloff01;
        body.AddForceAtPosition(impulse, impact.Position, ForceMode.Impulse);
    }
}
