using UnityEngine;
using Zenject;
using System.Linq;
using SUBS.Core.ResourceManagement;

namespace SUBS.Core
{
    public sealed class SLogger: IInitializable
    {
        [SerializeField] private static LoggerConfig _config;

        public async void Initialize()
        {
            //create default config, while custom config not loaded
            _config = ScriptableObject.CreateInstance<LoggerConfig>();
            _config.ConfigIsLoaded = false;

            _config = await AddressableObjectsLoader.LoadAssetAsync<LoggerConfig>(nameof(LoggerConfig));
            _config.ConfigIsLoaded = true;
        }

        #region Logs
        [HideInCallstack]
        public static void Log(string text, LogPriority priora = Core.LogPriority.low)
        {
#if UNITY_EDITOR
            if(_config.ShowLogsInEditor)
            {
                if(_config.Priority.Contains(priora))
                    Debug.Log(text);
            }
#else
            if(_config.ShowLogsInBuild)
            {
               if(_config.Priority.Contains(priora))   Debug.Log(text);
            }
#endif
        }

        [HideInCallstack]
        public static void LogEditorOnly(string text)
        {
            Debug.Log(text);
        }

        [HideInCallstack]
        public static void LogPriority(string text, LogPriority priora = Core.LogPriority.low)
        {
#if UNITY_EDITOR
            if(_config.ShowLogsInEditor)
            {
                if(_config.Priority.Contains(priora))
                    Debug.Log(text);
            }
#else
            if(_config.ShowLogsInBuild)
            {
                  if (_config.Priority.Contains(priora))   Debug.Log(text);
            }
#endif
        }

        [HideInCallstack]
        public static void Log(string text, Color color)
        {
#if UNITY_EDITOR
            if(_config.ShowLogsInEditor)
            {
                Debug.Log($"<color={color.ToHex()}>{text}</color>");
            }
#else
            if(_config.ShowLogsInBuild)
            {
                Debug.Log($"<color={color.ToHex()}>{text}</color>");
            }
#endif
        }

        [HideInCallstack]
        public static void Log(string text, CustomColorName colorName)
        {
#if UNITY_EDITOR
            if(_config.ShowLogsInEditor)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.Log($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.Log(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.Log(text);
                }
            }
#else
            if(_config.ShowLogsInBuild)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.Log($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.Log(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.Log(text);
                }
            }
#endif
        }

        #endregion Logs

        #region Warnings
        [HideInCallstack]
        public static void Warning(string text)
        {
#if UNITY_EDITOR
            if(_config.ShowWarningsInEditor)
            {
                Debug.LogWarning(text);
            }
#else
            if(_config.ShowWarningsInBuild)
            {
                Debug.LogWarning(text);
            }
#endif
        }

        [HideInCallstack]
        public static void Warning(string text, Color color)
        {
#if UNITY_EDITOR
            if(_config.ShowWarningsInEditor)
            {
                Debug.LogWarning($"<color={color.ToHex()}>{text}</color>");
            }
#else
            if(_config.ShowWarningsInBuild)
            {
                Debug.LogWarning($"<color={color.ToHex()}>{text}</color>");
            }
#endif
        }

        [HideInCallstack]
        public static void Warning(string text, CustomColorName colorName)
        {
#if UNITY_EDITOR
            if(_config.ShowWarningsInEditor)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.LogWarning($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.LogWarning(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.LogWarning(text);
                }
            }
#else
            if(_config.ShowWarningsInBuild)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.LogWarning($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.LogWarning(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.LogWarning(text);
                }
            }
#endif
        }

        #endregion Warnings

        #region Errors
        [HideInCallstack]
        public static void Error(string text)
        {
#if UNITY_EDITOR
            if(_config.ShowErrorsInEditor)
            {
                Debug.LogError(text);
            }
#else
            if(_config.ShowErrorsInBuild)
            {
                Debug.LogError(text);
            }
#endif
        }

        [HideInCallstack]
        public static void Error(string text, Color color)
        {
#if UNITY_EDITOR
            if(_config.ShowErrorsInEditor)
            {
                Debug.LogError($"<color={color.ToHex()}>{text}</color>");
            }
#else
            if(_config.ShowErrorsInBuild)
            {
                Debug.LogError($"<color={color.ToHex()}>{text}</color>");
            }
#endif
        }

        [HideInCallstack]
        public static void Error(string text, CustomColorName colorName)
        {
#if UNITY_EDITOR
            if(_config.ShowErrorsInEditor)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.LogError($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.LogError(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.LogError(text);
                }
            }
#else
            if(_config.ShowErrorsInBuild)
            {
                if(_config.ConfigIsLoaded)
                {
                    if(_config.LogColors.Length != 0)
                    {
                        Color color = _config.LogColors.Where(c => c.Name == colorName).FirstOrDefault().Color;
                        Debug.LogError($"<color={color.ToHex()}>{text}</color>");
                    }
                    else
                    {
                        Debug.Log("Please add colors to Logger Config. Executed default Unity log.");
                        Debug.LogError(text);
                    }
                }
                else
                {
                    Debug.Log("Logger Config still not loaded. Executed default Unity log.");
                    Debug.LogError(text);
                }
            }
#endif
        }

        #endregion Errors
    }
}