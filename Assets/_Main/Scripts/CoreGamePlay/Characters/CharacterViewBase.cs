using UnityEngine;
using Sirenix.OdinInspector;

namespace ZooWorld.CoreGamePlay
{
    [SelectionBase]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody))]
    public abstract class CharacterViewBase : SerializedMonoBehaviour
    {
        [SerializeReference,Required] protected ICharacter _character;
        public ICharacter Character => _character;
    }
}
