using System.Collections.Generic;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    public interface ICharacterView
    {
        ICharacter Character { get; }
        IReadOnlyList<DerivedCharacterTraitEntry> DerivedTraits { get; }
        Rigidbody RigidBody { get; }
    }
}
