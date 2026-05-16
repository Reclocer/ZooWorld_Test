using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CharacterViewBase : SerializedMonoBehaviour, ICharacterView
    {
        [SerializeField] protected Rigidbody _rigidbody;
        public Rigidbody RigidBody => _rigidbody;

        [SerializeReference, Required] protected CharacterBase _character;
        public ICharacter Character => _character;

        [Title("Derived skills")]
        [InfoBox("Read-only capabilities collected from components (run, swim, fly, etc.).")]
        [SerializeField, ReadOnly, ListDrawerSettings(IsReadOnly = true, ShowPaging = false)]
        protected List<DerivedCharacterTraitEntry> _derivedTraits = new();
        public IReadOnlyList<DerivedCharacterTraitEntry> DerivedTraits => _derivedTraits;

        #region MonoBehaviour
        protected virtual void OnValidate()
        {
            SubscribeToComponentChanges();
            RefreshTraits();
        }

        private void OnDestroy()
        {
            if (_character?.Components != null)
                _character.Components.OnChanged -= OnCharacterComponentsChanged;
        }

        protected virtual void Awake()
        {
            if (!_rigidbody)
                _rigidbody = GetComponent<Rigidbody>();

            if (_character == null)
                return;

            _character.SetView(this);
            SubscribeToComponentChanges();
            _character.InitializeComponents();
            RefreshTraits();
        }

        protected virtual void FixedUpdate()
        {
            SetMoveDirection(Vector3.forward);
            _character?.Components.Tick(Time.fixedDeltaTime);
        }
        #endregion MonoBehaviour

        /// <summary>
        /// Sets movement direction for this frame. Call from AI before physics tick if input drive is disabled.
        /// </summary>
        public void SetMoveDirection(Vector3 direction)
        {
            MovementComponent movement = _character?.Components.Get<MovementComponent>();
            movement?.SetMoveDirection(direction);
        }

        #region Traits
        public virtual bool HasTrait(CharacterTrait trait)
        {
            foreach (DerivedCharacterTraitEntry entry in _derivedTraits)
            {
                if (entry.Trait == trait)
                    return true;
            }

            return false;
        }

        protected virtual void SubscribeToComponentChanges()
        {
            if (_character?.Components == null)
                return;

            _character.Components.OnChanged -= OnCharacterComponentsChanged;
            _character.Components.OnChanged += OnCharacterComponentsChanged;
        }

        protected virtual void RefreshTraits()
        {
            if (_character == null)
            {
                _derivedTraits.Clear();
                return;
            }

            _derivedTraits = _character.Components.CollectTraitEntries();
        }

        protected virtual void OnCharacterComponentsChanged()
        {
            RefreshTraits();
        }
        #endregion Traits
    }
}
