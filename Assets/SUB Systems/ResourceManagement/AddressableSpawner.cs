using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace SUBS.Core.ResourceManagement
{
    public static class AddressableSpawner
    {        
        /// <summary>
        /// Spawns a GameObject from an AssetReference and returns the spawned GameObject. 
        /// The spawned GameObject will be parented to the specified Transform.
        /// </summary>
        public static async UniTask<GameObject> SpawnAsync(
            AssetReference assetReference, Transform parent)
        {
            AsyncOperationHandle<GameObject> instantiateHandle = 
                await SpawnWithHandleAsync(assetReference,parent);

            if (instantiateHandle.IsValid())
                return instantiateHandle.Result;

            return null;
        }

        /// <summary>
        /// Spawns a GameObject from an AssetReference and returns the AsyncOperationHandle. 
        /// The spawned GameObject will be parented to the specified Transform. 
        /// The caller is responsible for releasing the handle when done with the spawned object.
        /// </summary>
        public static async UniTask<AsyncOperationHandle<GameObject>> SpawnWithHandleAsync(
            AssetReference assetReference, Transform parent)
        {
            if (assetReference == null)
            {
                SLogger.Log("Asset reference = null");
                return default;
            }

            if (parent == null)
            {
                SLogger.Log("parent = null");
                return default;
            }

            AsyncOperationHandle<GameObject> instantiateHandle = assetReference.InstantiateAsync(parent);
            await instantiateHandle.Task;

            if (instantiateHandle.Status == AsyncOperationStatus.Succeeded)
            {
                return instantiateHandle;
            }
            else
            {
                Addressables.Release(instantiateHandle);
                SLogger.Error("Error spawning object: " + instantiateHandle.OperationException);
                return default;
            }
        }

        /// <summary>
        /// Spawns a GameObject from an AssetReference and injects dependencies using the provided DiContainer. 
        /// The spawned GameObject will be parented to the specified Transform.
        /// </summary>
        public static async UniTask<GameObject> SpawnAndInjectAsync(
            AssetReference assetReference,
            Transform parent,
            DiContainer container)
        {
            if (assetReference == null)
            {
                SLogger.Error("Asset reference = null");
                return null;
            }

            if (parent == null)
            {
                SLogger.Error("parent = null");
                return null;
            }

            if (container == null)
            {
                SLogger.Error("container = null");
                return null;
            }

            AsyncOperationHandle<GameObject> loadHandle = assetReference.LoadAssetAsync<GameObject>();
            await loadHandle.Task;

            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject prefab = loadHandle.Result;
                GameObject instance = container.InstantiatePrefab(prefab, parent);
                Addressables.Release(loadHandle);
                return instance;
            }
            else
            {
                Addressables.Release(loadHandle);
                SLogger.Error($"Error loading addressable: {loadHandle.OperationException}");
                return null;
            }
        }
    }
}