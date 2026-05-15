using System;
using System.Collections;
using UnityEngine;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace SUBS.Core
{
    public static class MonoBehaviourExtensions
    {
        #region Coroutines
        [Obsolete("Transition to " + nameof(DoAfterSecondsAsync), false)]
        public static IEnumerator DoAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, Action action)
        {
            bool done = false;

            try
            {
                yield return new WaitForSeconds(seconds);
                action();
                done = true;
            }
            finally
            {
                if (!done)
                    SLogger.Error("action cant start");
            }
        }

        [Obsolete("Transition to " + nameof(DoAfterSecondsAsync), false)]
        public static IEnumerator DoAfterSeconds(this MonoBehaviour monoBehaviour, float seconds, Action action, string nameOfClass)
        {
            bool done = false;

            try
            {
                yield return new WaitForSeconds(seconds);
                action();
                done = true;
            }
            finally
            {
                if (!done)
                    SLogger.Error($"{nameOfClass} action cant start");
            }
        }

        [Obsolete("Transition to " + nameof(WaitWhileAsync), false)]
        public static IEnumerator WaitWhile(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, Action action)
        {
            bool done = false;

            try
            {
                yield return new WaitWhile(whileTrue);
                action();
                done = true;
            }
            finally
            {
                if (!done)
                    SLogger.Error($"action cant start");
            }
        }

        [Obsolete("Transition to " + nameof(WaitSecondsAndWhileAsync), false)]
        public static IEnumerator WaitSecondsAndWhile(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue, Action action)
        {
            bool done = false;

            try
            {
                yield return new WaitForSeconds(seconds);
                yield return new WaitWhile(whileTrue);
                action();
                done = true;
            }
            finally
            {
                if (!done)
                    SLogger.Error($"action cant start");
            }
        }
        #endregion Coroutines

        #region Async
        #region DoAfterSeconds
        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, default, default, default, default, action).Forget();
        }

        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, string nameOfClass, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, default, default, default, nameOfClass, action).Forget();
        }

        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, CancellationToken token, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, token, default, default, default, action).Forget();
        }

        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, CancellationToken token, string nameOfClass, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, token, default, default, nameOfClass, action).Forget();
        }

        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, CancellationToken token,
            bool ignoreTimeScale, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, token, ignoreTimeScale, default, default, action).Forget();
        }

        public static void DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, CancellationToken token,
            bool ignoreTimeScale, string nameOfClass, Action action)
        {
            monoBehaviour.DoAfterSecondsAsync(seconds, token, ignoreTimeScale, default, nameOfClass, action).Forget();
        }

        public async static UniTask DoAfterSecondsAsync(this MonoBehaviour monoBehaviour, float seconds, CancellationToken token = default, bool ignoreTimeScale = false,
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
        public static void WaitWhileAsync(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, Action action)
        {
            monoBehaviour.WaitWhileAsync(whileTrue, default, default, default, action).Forget();
        }

        public static void WaitWhileAsync(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, string nameOfClass, Action action)
        {
            monoBehaviour.WaitWhileAsync(whileTrue, default, default, nameOfClass, action).Forget();
        }

        public static void WaitWhileAsync(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, CancellationToken token, Action action)
        {
            monoBehaviour.WaitWhileAsync(whileTrue, token, default, default, action).Forget();
        }

        public static void WaitWhileAsync(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, CancellationToken token, string nameOfClass, Action action)
        {
            monoBehaviour.WaitWhileAsync(whileTrue, token, default, nameOfClass, action).Forget();
        }

        public async static UniTask WaitWhileAsync(this MonoBehaviour monoBehaviour, Func<bool> whileTrue, CancellationToken token = default,
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
        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, default, default, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue, string nameOfClass, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, default, default, default, nameOfClass, action).Forget();
        }

        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue,
            CancellationToken token, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, token, default, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue,
            CancellationToken token, string nameOfClass, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, token, default, default, nameOfClass, action).Forget();
        }

        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue,
            CancellationToken token, bool ignoreTimeScale, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, token, ignoreTimeScale, default, default, action).Forget();
        }

        public static void WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue,
            CancellationToken token, bool ignoreTimeScale, string nameOfClass, Action action)
        {
            monoBehaviour.WaitSecondsAndWhileAsync(seconds, whileTrue, token, ignoreTimeScale, default, nameOfClass, action).Forget();
        }

        public async static UniTask WaitSecondsAndWhileAsync(this MonoBehaviour monoBehaviour, float seconds, Func<bool> whileTrue, CancellationToken token = default,
            bool ignoreTimeScale = false, PlayerLoopTiming playerLoopTiming = PlayerLoopTiming.Update, string nameOfClass = "", Action action = null)
        {
            await monoBehaviour.DoAfterSecondsAsync(seconds, token, ignoreTimeScale, playerLoopTiming, nameOfClass);
            await monoBehaviour.WaitWhileAsync(whileTrue, token, playerLoopTiming, nameOfClass);

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
        #endregion Async
    }
}