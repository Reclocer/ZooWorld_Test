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
        public override void Tick(Rigidbody rigidbody, float deltaTime, in MovementTickInfo info)
        {
            Vector3 vel = rigidbody.linearVelocity;
            Vector3 planarDir = new Vector3(info.MoveDirectionWorld.x, 0f, info.MoveDirectionWorld.z);

            Vector3 planarVel = new Vector3(vel.x, 0f, vel.z);
            Vector3 targetPlanar = planarDir * info.MaxSpeed;

            Vector3 deltaV = targetPlanar - planarVel;
            float maxDelta = info.Acceleration * deltaTime;

            if (deltaV.sqrMagnitude > maxDelta * maxDelta)
                deltaV = deltaV.normalized * maxDelta;

            Vector3 newPlanar = planarVel + deltaV;

            rigidbody.linearVelocity = new Vector3(newPlanar.x, vel.y, newPlanar.z);
        }
    }
}
