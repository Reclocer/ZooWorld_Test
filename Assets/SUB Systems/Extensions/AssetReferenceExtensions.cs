using Cysharp.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SUBS.Core
{
    public static class AssetReferenceExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static async UniTask<T> CustomInstantiateTAsync<T>(this AssetReferenceGameObject self,
            Transform parent = null, bool inWorldSpace = false) where T : MonoBehaviour
        {
            GameObject go = await self.InstantiateAsync(parent, inWorldSpace);
            T goComponent = go.GetComponentInChildren<T>(true);
            return goComponent;
        }
    }
}
