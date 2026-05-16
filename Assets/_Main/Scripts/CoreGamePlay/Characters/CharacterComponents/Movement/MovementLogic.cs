using System;
using UnityEngine;

namespace ZooWorld.CoreGamePlay
{
    /// <summary>
    /// Per-state movement strategy applied to a <see cref="Rigidbody"/> (Unity 6 <c>linearVelocity</c>).
    /// </summary>
    [Serializable]
    public abstract class MovementLogic
    {
        public abstract void Tick(Rigidbody body, float deltaTime, in MovementTickInfo info);
    }
}
