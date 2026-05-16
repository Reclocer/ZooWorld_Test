using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    internal static class MovementVelocityUtility
    {
        public static Vector3 GetPlanarDirection(Vector3 worldDirection)
        {
            Vector3 planar = new(worldDirection.x, 0f, worldDirection.z);            
            return planar.normalized;
        }

        public static void ApplyPlanarVelocity(Rigidbody body, float deltaTime, in MovementTickInfo info, Vector3 planarDirection)
        {
            Vector3 vel = body.linearVelocity;
            Vector3 planarDir = planarDirection.normalized;
            Vector3 planarVel = new(vel.x, 0f, vel.z);
            Vector3 targetPlanar = planarDir * info.MaxSpeed;
            Vector3 newPlanar = StepTowards(planarVel, targetPlanar, info.Acceleration * deltaTime);
            body.linearVelocity = new(newPlanar.x, vel.y, newPlanar.z);
        }

        public static void ApplyVelocity(Rigidbody body, float deltaTime, in MovementTickInfo info, Vector3 targetVelocity)
        {
            Vector3 vel = body.linearVelocity;
            Vector3 deltaV = targetVelocity - vel;
            float maxDelta = info.Acceleration * deltaTime;

            if (deltaV.magnitude > maxDelta)
                deltaV = deltaV.normalized * maxDelta;

            body.linearVelocity = vel + deltaV;
        }

        private static Vector3 StepTowards(Vector3 current, Vector3 target, float maxDelta)
        {
            Vector3 delta = target - current;

            if (delta.magnitude > maxDelta)
                delta = delta.normalized * maxDelta;

            return current + delta;
        }
    }
}
