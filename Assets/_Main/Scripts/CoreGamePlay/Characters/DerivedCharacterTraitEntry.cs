using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public struct DerivedCharacterTraitEntry
    {
        [ReadOnly, HideLabel, HorizontalGroup(0.35f)]
        [SerializeField] private CharacterTrait _trait;

        [ReadOnly, HideLabel, HorizontalGroup]
        [SerializeField] private string _source;

        public CharacterTrait Trait => _trait;
        public string Source => _source;

        public DerivedCharacterTraitEntry(CharacterTrait trait, string source)
        {
            _trait = trait;
            _source = source;
        }
    }
}
