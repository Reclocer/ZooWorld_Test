using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    [Serializable]
    public abstract class MovementLogic
    {
        public abstract CharacterTrait ProvidedTraits { get; }
        public abstract MovementType MovementType { get; }

        public abstract void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info);
    }
}
