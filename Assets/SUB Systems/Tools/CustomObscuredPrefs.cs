using CodeStage.AntiCheat.ObscuredTypes;
using CodeStage.AntiCheat.Storage;

namespace SUBS.Core
{
    public static class CustomObscuredPrefs
    {
        #region int
        public static void SetInt(string key, int value)
        {
            ObscuredPrefs.SetInt(key, value);
        }

        public static int GetInt(string key, int @default = 0)
        {
            return ObscuredPrefs.GetInt(key, @default);
        }

        public static void UpInt(string key, int upValue = 1)
        {
            int temp = GetInt(key);
            temp += upValue;
            SetInt(key, temp);
        }
        #endregion int

        #region float
        public static void SetFloat(string key, float value)
        {
            ObscuredPrefs.SetFloat(key, value);
        }

        public static float GetFloat(string key)
        {
            return ObscuredPrefs.GetFloat(key);
        }

        public static float GetFloat(string key, float @default = 0)
        {
            return ObscuredPrefs.GetFloat(key, @default);
        }

        public static void UpFloat(string key, float upValue = 1)
        {
            float temp = GetFloat(key);
            temp += upValue;
            SetFloat(key, temp);
        }
        #endregion float

        #region string
        public static void SetString(string key, string value)
        {
            ObscuredPrefs.SetString(key, value);
        }

        public static string GetString(string key)
        {
            return ObscuredPrefs.GetString(key);
        }
        #endregion string

        #region bool
        public static void SetBool(string key, bool value)
        {
            ObscuredPrefs.SetBool(key, value);
        }

        public static bool GetBool(string key, bool @default = false)
        {
            return ObscuredPrefs.GetBool(key, @default);
        }
        #endregion bool

        public static void DelPrefs()
        {
            ObscuredPrefs.DeleteAll();
        }

        public static void DelKey(string key)
        {
            ObscuredPrefs.DeleteKey(key);
        }
    }
}
