using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPhysics : MonoBehaviour
{
    public Vector3 Gravity = new Vector3(0f, -9.81f, 0f);

    private void Awake()
    {
        Physics.gravity = Gravity;
    }
}
