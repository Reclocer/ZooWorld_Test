using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class JumpLogic : MovementLogic
    {
        [SerializeField] private float _groundProbeDistance = 0.25f;
        [SerializeField] private float _groundProbeRadius = 0.2f;
        [SerializeField] private float _maxVerticalSpeedToLand = 0.25f;

        private bool _wasAirborne = true;

        public override MovementType MovementType => MovementType.Jump;
        public override CharacterTrait ProvidedTraits => CharacterTrait.CanJump;

        public override void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info)
        {
            Vector3 planarDir = MovementVelocityUtility.GetPlanarDirection(info.MoveDirectionWorld);
            MovementVelocityUtility.ApplyPlanarVelocity(body, deltaTime, in info, planarDir);

            Vector3 vel = body.linearVelocity;
            bool grounded = IsGrounded(body);
            bool verticallySettled = Mathf.Abs(vel.y) <= _maxVerticalSpeedToLand;

            if (!grounded || !verticallySettled)
            {
                _wasAirborne = true;
                return;
            }

            if (!_wasAirborne)
                return;

            float jumpSpeed = info.MaxSpeed;
            body.linearVelocity = new Vector3(vel.x, jumpSpeed, vel.z);
            _wasAirborne = false;
        }

        private bool IsGrounded(Rigidbody body)
        {
            Vector3 origin = body.worldCenterOfMass + Vector3.up * _groundProbeRadius;
            return Physics.SphereCast(
                origin,
                _groundProbeRadius,
                Vector3.down,
                out _,
                _groundProbeDistance + _groundProbeRadius,
                Physics.AllLayers,
                QueryTriggerInteraction.Ignore);
        }
    }
}
