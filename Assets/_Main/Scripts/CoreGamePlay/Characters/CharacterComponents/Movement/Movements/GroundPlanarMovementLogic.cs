using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    /// <summary>
    /// Walk / run / idle on the horizontal plane: adjusts XZ <c>linearVelocity</c>, preserves vertical (gravity, jumps).
    /// </summary>
    [Serializable]
    public class GroundPlanarMovementLogic : MovementLogic
    {
        public override void Tick(Rigidbody body, float deltaTime, in MovementTickContext context)
        {
            Vector3 vel = body.linearVelocity;
            Vector3 planarDir = new Vector3(context.MoveDirectionWorld.x, 0f, context.MoveDirectionWorld.z);

            if (planarDir.sqrMagnitude > 1e-6f)
                planarDir.Normalize();
            else
                planarDir = Vector3.zero;

            Vector3 planarVel = new Vector3(vel.x, 0f, vel.z);
            Vector3 targetPlanar = planarDir * context.MaxSpeed;

            Vector3 deltaV = targetPlanar - planarVel;
            float maxDelta = context.Acceleration * deltaTime;
            if (deltaV.sqrMagnitude > maxDelta * maxDelta)
                deltaV = deltaV.normalized * maxDelta;

            Vector3 newPlanar = planarVel + deltaV;

            if (planarDir.sqrMagnitude > 1e-6f && context.MaxSpeed > 1e-6f
                && newPlanar.sqrMagnitude < context.MaxSpeed * context.MaxSpeed * 0.25f)
            {
                newPlanar = planarDir * context.MaxSpeed;
            }

            body.linearVelocity = new Vector3(newPlanar.x, vel.y, newPlanar.z);
        }
    }
}
