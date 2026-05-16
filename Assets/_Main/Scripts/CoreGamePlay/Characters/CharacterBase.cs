using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public abstract class CharacterBase : ICharacter
    {
        [SerializeField] protected string _id;
        public string Id => _id;

        [SerializeField] protected ComponentsContainer _components = new();
        public ComponentsContainer Components => _components;

        protected ICharacterView _view;
        public ICharacterView View => _view;

        public void SetView(ICharacterView view)
        {
            _view = view;
        }

        public void InitializeComponents()
        {
            _components.Initialize(this);
        }
    }
}
