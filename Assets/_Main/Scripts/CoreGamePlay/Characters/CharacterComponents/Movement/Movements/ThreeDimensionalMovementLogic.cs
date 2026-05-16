using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    /// <summary>
    /// Swim / fly / dive style movement: drives full 3D <c>linearVelocity</c> toward <see cref="MovementTickContext.MoveDirectionWorld"/>.
    /// </summary>
    [Serializable]
    public class ThreeDimensionalMovementLogic : MovementLogic
    {
        public override void Tick(Rigidbody body, float deltaTime, in MovementTickContext context)
        {
            Vector3 dir = context.MoveDirectionWorld.sqrMagnitude > 1e-6f
                ? context.MoveDirectionWorld.normalized
                : Vector3.zero;

            Vector3 vel = body.linearVelocity;
            Vector3 target = dir * context.MaxSpeed;
            Vector3 deltaV = target - vel;
            float maxDelta = context.Acceleration * deltaTime;

            if (deltaV.sqrMagnitude > maxDelta * maxDelta)
                deltaV = deltaV.normalized * maxDelta;

            body.linearVelocity = vel + deltaV;
        }
    }
}
