using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public struct DerivedCharacterTraitEntry
    {
        [HideLabel, HorizontalGroup(0.35f)]
        [SerializeField,ReadOnly] private CharacterTrait _trait;

        [HideLabel, HorizontalGroup]
        [SerializeField,ReadOnly] private string _source;

        public CharacterTrait Trait => _trait;
        public string Source => _source;

        public DerivedCharacterTraitEntry(CharacterTrait trait, string source)
        {
            _trait = trait;
            _source = source;
        }
    }
}
