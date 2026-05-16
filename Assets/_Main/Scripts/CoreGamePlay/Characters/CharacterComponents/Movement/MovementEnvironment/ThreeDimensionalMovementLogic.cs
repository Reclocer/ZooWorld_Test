using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    /// <summary>
    /// Swim / fly / dive style movement: drives full 3D <c>linearVelocity</c> toward <see cref="MovementTickInfo.MoveDirectionWorld"/>.
    /// </summary>
    [Serializable]
    public class ThreeDimensionalMovementLogic : MovementLogic
    {
        public override void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info)
        {
            Vector3 dir = info.MoveDirectionWorld.normalized;

            Vector3 vel = body.linearVelocity;
            Vector3 target = dir * info.MaxSpeed;
            Vector3 deltaV = target - vel;
            float maxDelta = info.Acceleration * deltaTime;

            if (deltaV.sqrMagnitude > maxDelta * maxDelta)
                deltaV = deltaV.normalized * maxDelta;

            body.linearVelocity = vel + deltaV;
        }
    }
}
