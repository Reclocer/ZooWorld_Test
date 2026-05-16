using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class MovementState
    {
        [Serializable]
        public class OutgoingTransition
        {
            [Required, PropertyTooltip("Target state id when the condition becomes true.")]
            [SerializeField] private string _toStateId;

            [PropertyTooltip("World / AI signal that triggers this transition.")]
            [SerializeField] private MovementTransitionCondition _condition = MovementTransitionCondition.ThreatDetected;

            [PropertyTooltip("Higher value wins when several transitions from this state match in the same FixedUpdate.")]
            [SerializeField] private int _priority;

            public string ToStateId => _toStateId;
            public MovementTransitionCondition Condition => _condition;
            public int Priority => _priority;
        }

        [Required, PropertyTooltip("Unique id (e.g. Idle, Run, Swim).")]
        [SerializeField] private string _id = "Idle";

        [PropertyTooltip("Locomotion kind — defines which character traits this state grants.")]
        [SerializeField] private MovementType _locomotionType = MovementType.Idle;

        [PropertyTooltip("Designer speed (× VelocityScale 1000 at runtime, e.g. 5 → 5000 u/s).")]
        [SerializeField, Range(0f, 100f)] private float _maxSpeed = 5f;

        [PropertyTooltip("Designer acceleration (× VelocityScale 1000 at runtime).")]
        [SerializeField, Range(0f, 100f)] private float _acceleration = 8f;

        [PropertyTooltip("Used as default on play if no other state is active.")]
        [SerializeField] private bool _isDefault;

        [Title("Movement")]
        [InfoBox("Assign SerializeReference implementation: GroundPlanar (walk/run) or ThreeDimensional (swim/fly).")]
        [SerializeReference, Required]
        [PropertyTooltip("Rigidbody integration for this state (speed/acceleration come from fields above).")]
        private MovementLogic _movementLogic;

        [Title("Transitions from this state")]
        [InfoBox("Leave as Idle → Run on ThreatDetected. Transitions are evaluated only while this state is active.")]
        [SerializeField, ListDrawerSettings(DefaultExpandedState = true, ShowPaging = false)]
        private List<OutgoingTransition> _outgoingTransitions = new();

        public string Id => _id;
        public MovementType LocomotionType => _locomotionType;
        public float MaxSpeed => _maxSpeed;
        public float Acceleration => _acceleration;
        public bool IsDefault => _isDefault;
        public MovementLogic MovementLogic => _movementLogic;
        public IReadOnlyList<OutgoingTransition> OutgoingTransitions => _outgoingTransitions;
    }
}
