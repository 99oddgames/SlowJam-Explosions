using System.Collections.Generic;
using UnityEngine;

public struct GameplayVolumeOverlap<T>
{
    public T Component;

    public Vector3 Position;
    public Vector3 DirectionAwayNormalized;

    public float Radius;
    public float DistanceFromCenter;
    public float DistanceFromCenter01;

    public void DebugDraw(float duration)
    {
        const float lineLength = 5f;
        Debug.DrawLine(Position, Position + Vector3.up * lineLength, Color.red, duration);

        var awayColor = Color.Lerp(Color.red, Color.black, DistanceFromCenter01);
        Debug.DrawLine(Position, Position + DirectionAwayNormalized * lineLength, awayColor, duration);
    }
}

public class GameplayVolume : MonoBehaviour
{
    public float Radius = 3f;

    private const int maxCollidersInQuery = 300;
    private Collider[] colliderHits = new Collider[maxCollidersInQuery];
    
    public bool OverlapVolume<T>(in List<GameplayVolumeOverlap<T>> overlaps) where T : Component
    {
        overlaps.Clear();
        var count = Physics.OverlapSphereNonAlloc(transform.position, Radius, colliderHits);

        for (int i = 0; i < count; i++)
        {
            var nextGameObject = colliderHits[i].gameObject;
            var nextCollider = colliderHits[i];

            if (!nextGameObject.TryGetComponent<T>(out var nextComponent))
                continue;

            var position = nextCollider.ClosestPointOnSurface(transform.position);
            var away = position - transform.position;
            var distance = away.magnitude;
            var distance01 = Mathf.Clamp01(distance / Radius);

            GameplayVolumeOverlap<T> nextOverlap = new GameplayVolumeOverlap<T>()
            {
                Component = nextComponent,
                Position = position,
                Radius = this.Radius,
                DirectionAwayNormalized = distance > 1E-05f ? away / distance : Vector3.zero,
                DistanceFromCenter = distance,
                DistanceFromCenter01 = distance01,
            };

            overlaps.Add(nextOverlap);
        }

        return overlaps.Count > 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
