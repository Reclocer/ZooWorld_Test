using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class MoveLogic : MovementLogic
    {
        public override CharacterTrait ProvidedTraits => CharacterTrait.CanRun;
        public override MovementType MovementType => MovementType.Run;

        public override void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info)
        {
            Vector3 planarDir = MovementVelocityUtility.GetPlanarDirection(info.MoveDirectionWorld);
            MovementVelocityUtility.ApplyPlanarVelocity(body, deltaTime, in info, planarDir);
        }
    }
}
