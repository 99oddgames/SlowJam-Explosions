using UnityEngine;

namespace GameEvents
{
    public struct MouseClick
    {
        public Vector3 WorldPosition;
    }

    public struct ForceImpact
    {
        public Vector3 DirectionNormalized;
        public float Force;
    }

    public struct BetterForceImpact
    {
        public Vector3 Position;
        public Vector3 DirectionNormalized;
        public Vector3 DirectionalStrength;

        public float Force;
        public float Falloff01;
        public float UpwardBias01;

        public bool ResetVelocity;
        public bool InvertDirection;
    }
}
