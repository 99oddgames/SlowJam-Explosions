using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Section3 : GameplayObject
{
    public Delay Duration = new Delay(1f);
    public GameplayVolume Volume;

    private List<GameplayVolumeOverlap<GameplayObject>> hits = new List<GameplayVolumeOverlap<GameplayObject>>(32);

    private void Start()
    {
        Duration.Next();

        if(Volume.OverlapVolume(hits))
        {
            for(int i = 0; i < hits.Count; i++)
            {
                hits[i].DebugDraw(5f);
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
