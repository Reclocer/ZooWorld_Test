using Cysharp.Threading.Tasks;
using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SUBS.Core.ResourceManagement
{
    public static class AddressableObjectsLoader
    {
        public static async UniTask<T> LoadAssetAsync<T>(string address) 
            where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(address);

            try
            {
                await handle.Task;
                return handle.Result;
            }
            catch (Exception e)
            {
                SLogger.Error($"Failed to load asset from Addressables: {e.Message}");
                Addressables.Release(handle);
                return null;
            }
        }

        public static async UniTask<AsyncOperationHandle<T>> LoadAssetAsyncHandle<T>(string address) 
            where T : UnityEngine.Object
        {
            var handle = Addressables.LoadAssetAsync<T>(address);

            await handle.Task;

            return handle;
        }
    }
}