using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Section4 : GameplayObject
{
    public Delay Duration = new Delay(1f);
    public GameplayVolume Volume;

    public float ForceAlongAwayVector;

    private List<GameplayVolumeOverlap<GameplayObject>> hits = new List<GameplayVolumeOverlap<GameplayObject>>(32);

    private void Start()
    {
        Duration.Next();

        if(Volume.OverlapVolume(hits))
        {
            for(int i = 0; i < hits.Count; i++)
            {
                var nextHit = hits[i];

                nextHit.Component.SendMessage(new GameEvents.ForceImpact()
                {
                    DirectionNormalized = nextHit.DirectionAwayNormalized,
                    Force = ForceAlongAwayVector,
                });
            }
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
