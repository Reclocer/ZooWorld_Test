using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace SUBS.Core
{
    public static class AsyncExtensions
    {
        #region DoAfterSeconds
        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, default, default, default, default, action).Forget();
        }

        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, string nameOfClass, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, default, default, default, nameOfClass, action).Forget();
        }

        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, CancellationToken token, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, token, default, default, default, action).Forget();
        }

        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, CancellationToken token, string nameOfClass, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, token, default, default, nameOfClass, action).Forget();
        }

        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, CancellationToken token,
            bool ignoreTimeScale, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, token, ignoreTimeScale, default, default, action).Forget();
        }

        public static void DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, CancellationToken token,
            bool ignoreTimeScale, string nameOfClass, Action action)
        {
            iHaveAsyncAction.DoAfterSeconds(seconds, token, ignoreTimeScale, default, nameOfClass, action).Forget();
        }

        public async static UniTask DoAfterSeconds(this IHaveAsyncAction iHaveAsyncAction, float seconds, CancellationToken token = default, bool ignoreTimeScale = false,
            PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, string nameOfClass = "", Action action = null)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(seconds), ignoreTimeScale, playerLoopTiming, token);

            try
            {
                if (action != null)
                    action();
            }
            catch
            {
                if (nameOfClass != "")
                {
                    SLogger.Error($"{nameOfClass} action cant start");
                }
                else
                {
                    SLogger.Error("Action cant start");
                }
            }
        }
        #endregion DoAfterSeconds

        #region WaitWhile
        public static void WaitWhile(this IHaveAsyncAction iHaveAsyncAction, Func<bool> whileTrue, Action action)
        {
            iHaveAsyncAction.WaitWhile(whileTrue, default, default, default, action).Forget();
        }

        public static void WaitWhile(this IHaveAsyncAction iHaveAsyncAction, Func<bool> whileTrue, string nameOfClass, Action action)
        {
            iHaveAsyncAction.WaitWhile(whileTrue, default, default, nameOfClass, action).Forget();
        }

        public static void WaitWhile(this IHaveAsyncAction iHaveAsyncAction, Func<bool> whileTrue, CancellationToken token, Action action)
        {
            iHaveAsyncAction.WaitWhile(whileTrue, token, default, default, action).Forget();
        }

        public static void WaitWhile(this IHaveAsyncAction iHaveAsyncAction, Func<bool> whileTrue, CancellationToken token, string nameOfClass, Action action)
        {
            iHaveAsyncAction.WaitWhile(whileTrue, token, default, nameOfClass, action).Forget();
        }

        public async static UniTask WaitWhile(this IHaveAsyncAction iHaveAsyncAction, Func<bool> whileTrue, CancellationToken token = default,
            PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, string nameOfClass = "", Action action = null)
        {
            await UniTask.WaitWhile(whileTrue, playerLoopTiming, token);

            try
            {
                if (action != null)
                    action();
            }
            catch
            {
                if (nameOfClass != "")
                {
                    SLogger.Error($"{nameOfClass} action cant start");
                }
                else
                {
                    SLogger.Error("Action cant start");
                }
            }
        }
        #endregion WaitWhile

        #region WaitSecondsAndWhile
        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, default, default, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue, string nameOfClass, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, default, default, default, nameOfClass, action).Forget();
        }

        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue,
            CancellationToken token, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, token, default, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue,
            CancellationToken token, string nameOfClass, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, token, default, default, nameOfClass, action).Forget();
        }

        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue,
            CancellationToken token, bool ignoreTimeScale, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, token, ignoreTimeScale, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue,
            CancellationToken token, bool ignoreTimeScale, string nameOfClass, Action action)
        {
            iHaveAsyncAction.WaitSecondsAndWhile(seconds, whileTrue, token, ignoreTimeScale, default, nameOfClass, action).Forget();
        }

        public async static UniTask WaitSecondsAndWhile(this IHaveAsyncAction iHaveAsyncAction, float seconds, Func<bool> whileTrue, CancellationToken token = default,
            bool ignoreTimeScale = false, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, string nameOfClass = "", Action action = null)
        {
            await iHaveAsyncAction.DoAfterSeconds(seconds, token, ignoreTimeScale, playerLoopTiming, nameOfClass);
            await iHaveAsyncAction.WaitWhile(whileTrue, token, playerLoopTiming, nameOfClass);

            try
            {
                action();
            }
            catch
            {
                if (nameOfClass != "")
                {
                    SLogger.Error($"{nameOfClass} action cant start");
                }
                else
                {
                    SLogger.Error("Action cant start");
                }
            }
        }
        #endregion WaitSecondsAndWhile

        #region Addressables
        /// <summary>
        /// Simple usage examples:
        /// 1. Use in sync methods: _spriteReference.LoadResourceAsync<Sprite>((s) => { _image.sprite = s; });
        /// 2. Use in async methods: Sprite sprite = await _spriteReference.LoadResourceAsync<Sprite>();
        /// </summary>    
        public static async UniTask<T> LoadResourceAsync<T>(
            this AssetReference reference,
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
            this AssetReference reference,
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
        #endregion Addressables
    }
}
