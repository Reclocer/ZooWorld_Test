using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class MovementComponent : CharacterComponentBase, ICharacterTraitProvider, ICharacterTickable
    {     
        [PropertyTooltip("Current movement state id (play mode).")]
        [ShowInInspector, ReadOnly] private string _activeStateId;
        public string ActiveStateId => _activeStateId;

        [PropertyTooltip("World signals consumed by transition conditions.")]
        [ShowInInspector, ReadOnly] private StateConditionInfo _conditionInfo = new();
        public StateConditionInfo StateConditionInfo => _conditionInfo;

        [PropertyTooltip("Direction passed into MovementLogic this frame (zero = no horizontal drive).")]
        [ShowInInspector, ReadOnly] private Vector3 _debugMoveDirection;

        [Space]
        [ListDrawerSettings(DefaultExpandedState = true, ShowPaging = false)]
        [SerializeField] private List<MovementState> _states = new();

        private readonly Dictionary<string, MovementState> _stateById = new(StringComparer.Ordinal);

        private MovementState _activeState;
        public MovementState ActiveState => _activeState;

        private bool _initialized;
        private bool _wasInWater;
        private bool _wasUnderWater;
        private Vector3 _moveDirectionWorld;

        public float CurrentMaxSpeed => (_activeState?.MaxSpeed ?? 0f) * Constants.Movement.VELOCITY_SCALE;
        public float CurrentAcceleration => (_activeState?.Acceleration ?? 0f) * Constants.Movement.VELOCITY_SCALE;
        public MovementType ActiveLocomotionType => _activeState?.MovementType ?? MovementType.Idle;
              
        public override void Init(ICharacter character)
        {
            base.Init(character);
            RebuildStateLookup();
            _activeState = ResolveDefaultState();
            _activeStateId = _activeState?.Id;
            _initialized = true;
        }

        public void Tick(float fixedDeltaTime)
        {
            if (!_initialized)
                return;

            EvaluateTransitions();
            RunActiveMovementLogic(fixedDeltaTime);
            CacheSignalEdges();
        }

        public void SetMoveDirection(Vector3 worldDirection)
        {
            if (worldDirection.sqrMagnitude < 1e-6f)
            {
                _moveDirectionWorld = Vector3.zero;
                _debugMoveDirection = Vector3.zero;
                return;
            }

            _moveDirectionWorld = worldDirection.normalized;
            _debugMoveDirection = _moveDirectionWorld;
        }        

        public bool TrySetState(string stateId, bool force = false)
        {
            if (string.IsNullOrEmpty(stateId) || !_stateById.TryGetValue(stateId, out MovementState state))
                return false;

            if (!force && _activeState != null && _activeState.Id == stateId)
                return true;

            _activeState = state;
            _activeStateId = state.Id;
            return true;
        }

        public void SetThreatDetected(bool value)
        {
            _conditionInfo.ThreatDetected = value;
        }

        public void SetInWater(bool inWater, bool underWater)
        {
            _conditionInfo.InWater = inWater;
            _conditionInfo.UnderWater = underWater;
        }

        private void EvaluateTransitions()
        {
            if (_activeState == null)
                return;

            IReadOnlyList<MovementState.OutgoingTransition> outgoing = _activeState.OutgoingTransitions;

            if (outgoing == null || outgoing.Count == 0)
                return;

            MovementState.OutgoingTransition best = null;

            foreach (MovementState.OutgoingTransition transition in outgoing.OrderByDescending(t => t.Priority))
            {
                if (!EvaluateCondition(transition.Condition))
                    continue;

                best = transition;
                break;
            }

            if (best != null)
                TrySetState(best.ToStateId);
        }

        private bool EvaluateCondition(MovementTransitionCondition condition)
        {
            return condition switch
            {
                MovementTransitionCondition.None => false,
                MovementTransitionCondition.ThreatDetected => _conditionInfo.ThreatDetected,
                MovementTransitionCondition.ThreatCleared => !_conditionInfo.ThreatDetected,
                MovementTransitionCondition.EnteredWater => _conditionInfo.InWater && !_wasInWater,
                MovementTransitionCondition.ExitedWater => !_conditionInfo.InWater && _wasInWater,
                MovementTransitionCondition.InWater => _conditionInfo.InWater,
                MovementTransitionCondition.UnderWater => _conditionInfo.UnderWater,
                MovementTransitionCondition.LeftUnderWater => !_conditionInfo.UnderWater && _wasUnderWater,
                MovementTransitionCondition.Manual => false,
                _ => false,
            };
        }

        private void RunActiveMovementLogic(float deltaTime)
        {
            Rigidbody body = Character?.View?.RigidBody;

            if (body == null || _activeState == null)
                return;

            MovementLogic logic = _activeState.MovementLogic
                ?? MovementLogicDefaults.Resolve(_activeState.MovementType);

            MovementTickInfo info = MovementTickInfo.FromState(_moveDirectionWorld, _activeState);
            logic.Tick(body, deltaTime, in info);
        }

        private void RebuildStateLookup()
        {
            _stateById.Clear();

            if (_states == null)
                return;

            foreach (MovementState state in _states)
            {
                if (state == null || string.IsNullOrEmpty(state.Id))
                    continue;

                _stateById[state.Id] = state;
            }
        }

        private MovementState ResolveDefaultState()
        {
            if (_states == null || _states.Count == 0)
                return null;

            MovementState markedDefault = _states.FirstOrDefault(s => s != null && s.IsDefault);
            return markedDefault ?? _states.FirstOrDefault(s => s != null);
        }

        private void CacheSignalEdges()
        {
            _wasInWater = _conditionInfo.InWater;
            _wasUnderWater = _conditionInfo.UnderWater;
        }

        #region About trait
        public IEnumerable<CharacterTrait> GetTraits()
        {
            var combined = CharacterTrait.None;

            foreach (MovementState state in _states)
                combined |= MovementTraits.ToTrait(state.MovementType);

            foreach (CharacterTrait flag in Enum.GetValues(typeof(CharacterTrait)))
            {
                if (flag == CharacterTrait.None)
                    continue;

                if ((combined & flag) != 0)
                    yield return flag;
            }
        }

        public bool HasTrait(CharacterTrait trait)
        {
            foreach (MovementState state in _states)
            {
                CharacterTrait stateTraits = MovementTraits.ToTrait(state.MovementType);
                if ((stateTraits & trait) != 0)
                    return true;
            }

            return false;
        }
        #endregion About trait

#if UNITY_EDITOR
        [OnInspectorInit]
        private void EditorRebuildLookup()
        {
            RebuildStateLookup();
        }
#endif
    }
}
