using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SUBS.Core
{
    public static class BaseExtensions
    {
        public static TEnum ToEnum<TEnum>(this string strEnumValue, TEnum defaultValue, bool showLogs = true)
        {
            if(!Enum.IsDefined(typeof(TEnum), strEnumValue))
            {
                if(showLogs)
                    SLogger.Error($"Enum element not found {strEnumValue} in {nameof(TEnum)}. Setted default {defaultValue}");

                return defaultValue;
            }

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }

        public static int GetLenght<T>(this Enum targetEnum, T enumType) where T : Enum
        {
            return Enum.GetNames(typeof(T)).Length;
        }

        /// <summary>
        /// Get Random Point In LINE Upper Hemisphere
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector2 GetRandomPointInUpperHemisphere(this Vector2 center, float radius)
        {
            float theta = Random.Range(0f, Mathf.PI);
            float x = center.x + radius * Mathf.Cos(theta);
            float y = center.y + radius * Mathf.Sin(theta);
            return new Vector2(x, y);
        }

        /// <summary>
        /// Get Random Point In INSIDE Upper Hemisphere
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public static Vector2 GetRandomPointInUpperHemisphereInside(this Vector2 center, float radius)
        {
            float theta = Random.Range(0f, Mathf.PI);
            radius *= Mathf.Sqrt(Random.Range(0f, 1f)); // для точки внутри полусферы
            float x = center.x + radius * Mathf.Cos(theta);
            float y = center.y + radius * Mathf.Sin(theta);
            return new Vector2(x, y);
        }

        public static string Color(this string need, Color color)
        {
            return $"<color={color.ToHex()}>{need}</color>";
        }

        public static string ToHex(this Color color)
        {
            string rtn = $"#{(int)(color.r * 255):X2}{(int)(color.g * 255):X2}{(int)(color.b * 255):X2}";
            return rtn;
        }
    }
}