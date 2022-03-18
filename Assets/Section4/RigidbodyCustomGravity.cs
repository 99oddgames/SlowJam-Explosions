using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyCustomGravity : GameplayComponent
{
    public float GravityScaleMovingDown = 1f;
    public bool ApplyGlobalGravity = true;

    private Rigidbody body;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var customGravity = Physics.gravity;

        if(body.velocity.y < 0)
        {
            customGravity.y *= GravityScaleMovingDown;
        }

        body.AddForce(customGravity, ForceMode.Acceleration);
        body.useGravity = ApplyGlobalGravity;
    }
}
