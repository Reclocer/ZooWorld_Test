using System;
using System.Collections.Generic;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public abstract class CharacterComponentBase : ICharacterComponent
    {
        protected ICharacter Character { get; private set; }

        public virtual void Init(ICharacter character)
        {
            Character = character;
        }
    }
}
