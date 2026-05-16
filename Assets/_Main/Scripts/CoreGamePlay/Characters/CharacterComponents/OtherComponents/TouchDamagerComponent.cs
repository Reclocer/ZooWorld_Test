using System;
using System.Collections.Generic;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public class TouchDamagerComponent : CharacterComponentBase, ICharacterTraitProvider
    {
        public IEnumerable<CharacterTrait> GetTraits()
        {
            yield return CharacterTrait.DealsTouchDamage;
        }
    }
}
