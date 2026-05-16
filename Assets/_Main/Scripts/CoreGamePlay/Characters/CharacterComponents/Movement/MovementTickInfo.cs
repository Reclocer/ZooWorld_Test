using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    public readonly struct MovementTickInfo
    {
        public readonly Vector3 MoveDirectionWorld;
        public readonly float MaxSpeed;
        public readonly float Acceleration;

        public MovementTickInfo(Vector3 moveDirectionWorld, float maxSpeed, float acceleration)
        {
            MoveDirectionWorld = moveDirectionWorld;
            MaxSpeed = maxSpeed * Constants.Movement.VELOCITY_SCALE;
            Acceleration = acceleration * Constants.Movement.VELOCITY_SCALE;
        }

        public static MovementTickInfo FromState(Vector3 moveDirectionWorld, MovementState state)
        {
            return new MovementTickInfo(moveDirectionWorld, state.MaxSpeed, state.Acceleration);
        }
    }
}
