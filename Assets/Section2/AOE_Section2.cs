using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOE_Section2 : GameplayObject
{
    public Delay Duration = new Delay(1f);

    private void Start()
    {
        Duration.Next();
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
