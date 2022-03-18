using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColliderExtensions
{
    //TODO: this implementation is very naive, google around for something better
    public static Vector3 ClosestPointOnSurface(this Collider collider, Vector3 point)
    {
        const float largeDistance = 100f;
        var away = point - collider.transform.position;
        return collider.ClosestPoint(point + away * largeDistance);
    }
}
