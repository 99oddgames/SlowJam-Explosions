using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Section6 : GameplayObject
{
    public Delay Duration = new Delay(1f);
    public GameplayVolume Volume;

    public float ForceAlongAwayVector;
    [Range(0, 1f)]
    public float UpwardBias;
    public Vector3 DirectionalStrength = Vector3.one;
    public AnimationCurve DistanceFalloff = new AnimationCurve(new Keyframe(0.0f, 1f));
    public bool ResetVelocity;
    public bool InvertDirection;

    [Header("Effects")]
    public VFX BlastVFX;

    private List<GameplayVolumeOverlap<GameplayObject>> hits = new List<GameplayVolumeOverlap<GameplayObject>>(32);

    private void Start()
    {
        Duration.Next();

        if(Volume.OverlapVolume(hits))
        {
            for(int i = 0; i < hits.Count; i++)
            {
                var nextHit = hits[i];

                nextHit.Component.SendMessage(new GameEvents.BetterForceImpact()
                {
                    Position = nextHit.Position,
                    DirectionNormalized = nextHit.DirectionAwayNormalized,
                    Force = ForceAlongAwayVector,
                    DirectionalStrength = this.DirectionalStrength,
                    Falloff01 = DistanceFalloff.Evaluate(1f - nextHit.DistanceFromCenter01),
                    UpwardBias01 = UpwardBias,
                    ResetVelocity = this.ResetVelocity,
                    InvertDirection = this.InvertDirection
                });
            }
        }

        if(BlastVFX)
        {
            Instantiate(BlastVFX, transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if(Duration.IsUp)
        {
            Destroy(gameObject);
            return;
        }
    }
}
