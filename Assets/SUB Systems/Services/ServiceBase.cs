using Cysharp.Threading.Tasks;
using UnityEngine;

#pragma warning disable CS1998

namespace SUBS.Core
{
    public abstract class ServiceBase : IService
    {
        public virtual async UniTask Init()
        {
            Debug.Log($"[Service] Init ServiceBase {this}");
        }
    }
}
