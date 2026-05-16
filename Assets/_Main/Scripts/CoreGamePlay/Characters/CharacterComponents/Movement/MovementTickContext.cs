using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    /// <summary>
    /// Snapshot of inputs and state parameters passed into <see cref="MovementLogic"/> each physics step.
    /// </summary>
    public readonly struct MovementTickContext
    {
        public readonly Vector3 MoveDirectionWorld;
        public readonly float MaxSpeed;
        public readonly float Acceleration;

        public MovementTickContext(Vector3 moveDirectionWorld, float maxSpeed, float acceleration)
        {
            MoveDirectionWorld = moveDirectionWorld;
            MaxSpeed = maxSpeed * Constants.Movement.VELOCITY_SCALE;
            Acceleration = acceleration * Constants.Movement.VELOCITY_SCALE;
        }

        public static MovementTickContext FromState(Vector3 moveDirectionWorld, MovementState state)
        {
            return new MovementTickContext(moveDirectionWorld, state.MaxSpeed, state.Acceleration);
        }
    }
}
