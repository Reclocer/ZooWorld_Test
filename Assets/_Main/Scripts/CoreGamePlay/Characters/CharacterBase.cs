using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public abstract class CharacterBase : ICharacter
    {
        [SerializeField] protected string _id;
        public string Id => _id;

        [SerializeField] protected ComponentsContainer _components;

        protected ICharacterView _view;
        public ICharacterView View => _view;
    }
}
