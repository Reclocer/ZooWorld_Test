using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

#pragma warning disable CS1998

namespace SUBS.Core
{
    public abstract class MonoBehaviourServiceBase : SerializedMonoBehaviour, IService
    {
        [SerializeField] protected bool _showLogs = true;

        public virtual async UniTask Init()
        {
            if (_showLogs)
                Debug.Log($"[Service] Init ServiceBase {gameObject.name}");
        }
    }
}
