using System;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine;

namespace SUBS.Core.ResourceManagement
{
    public class AddressableResourceLoader
    {
        /// <summary>
        /// Simple usage examples:
        /// 1. Use in sync methods: _spriteReference.LoadResourceAsync<Sprite>((s) => { _image.sprite = s; });
        /// 2. Use in async methods: Sprite sprite = await _spriteReference.LoadResourceAsync<Sprite>();
        /// </summary>    
        public static async UniTask<T> LoadResourceAsynñ<T>(
            AssetReference reference,
            Action<T> onLoad = null,
            CancellationToken token = default)
        {
            if (reference == null)
            {
                SLogger.Error("AssetReference is null!");
                return default;
            }

            try
            {
                T result = await reference
                    .LoadAssetAsync<T>()
                    .ToUniTask()
                    .AttachExternalCancellation(token);

                onLoad?.Invoke(result);
                return result;
            }
            catch (OperationCanceledException)
            {
                SLogger.Log("Resource loading was cancelled.");
                throw;
            }
            catch (Exception e)
            {
                SLogger.Error($"Resource failed to load: {e}");
                return default;
            }
        }

        /// <summary>
        /// Simple usage examples:
        /// 1. Use in sync methods: _spriteReference.LoadResourceAsync<Sprite>(this, (s) => { _image.sprite = s; });
        /// 2. Use in async methods: Sprite sprite = await _spriteReference.LoadResourceAsync<Sprite>(this);
        /// </summary>
        public static async UniTask<T> LoadResourceAsync_ReleaseOnDestroy<T>(
            AssetReference reference,
            MonoBehaviour owner,
            Action<T> onLoad = null,
            CancellationToken token = default)
        {
            if (reference == null)
            {
                SLogger.Error("AssetReference is null!");
                return default;
            }

            if (owner == null)
            {
                SLogger.Error("Owner (MonoBehaviour) is null!");
                return default;
            }

            AsyncOperationHandle<T> handle = default;

            try
            {
                handle = reference.LoadAssetAsync<T>();

                T result = await handle
                    .ToUniTask()
                    .AttachExternalCancellation(token);

                onLoad?.Invoke(result);

                ReleaseOnDestroy(owner, handle).Forget();

                return result;
            }
            catch (OperationCanceledException)
            {
                SLogger.Log("Resource loading was cancelled.");

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }

                throw;
            }
            catch (Exception e)
            {
                SLogger.Error($"Resource failed to load: {e}");

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }

                return default;
            }
        }

        private static async UniTaskVoid ReleaseOnDestroy<T>(
            MonoBehaviour owner,
            AsyncOperationHandle<T> handle)
        {
            try
            {
                await UniTask.WaitUntilCanceled(owner.GetCancellationTokenOnDestroy());

                if (handle.IsValid())
                {
                    Addressables.Release(handle);
                }
            }
            catch (Exception e)
            {
                SLogger.Error($"ReleaseOnDestroy exception: {e}");
            }
        }
    }
}
