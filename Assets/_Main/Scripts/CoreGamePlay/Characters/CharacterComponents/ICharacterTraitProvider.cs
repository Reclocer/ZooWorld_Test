using System.Collections.Generic;

namespace ZooWorld.CoreGamePlay
{
    public interface ICharacterTraitProvider
    {
        IEnumerable<CharacterTrait> GetTraits();
    }
}
