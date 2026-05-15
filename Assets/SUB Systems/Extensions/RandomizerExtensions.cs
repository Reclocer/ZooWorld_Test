using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SUBS.Core
{
    public static class RandomizerExtensions
    {
        public static TEnum GetRandomEnum<TEnum>(this TEnum @enum, params TEnum[] enums) where TEnum : Enum
        {
            int num = Random.Range(0, enums.Length);
            return enums[num];
        }

        /// <summary>
        /// Return one random selected value from array
        /// </summary>
        public static int GetRandomIntValue(this int value, params int[] values)
        {
            int num = Random.Range(0, values.Length);
            return values[num];
        }

        /// <summary>
        /// Return one random selected value from array
        /// </summary>
        public static float GetRandomFloatValue(this float value, params float[] values)
        {
            int num = Random.Range(0, values.Length);
            return values[num];
        }

        public static Vector2 RandomRangeVector2(this Vector2 randomVector, Vector2 vector1, Vector2 vector2)
        {
            float x = Random.Range(vector1.x, vector2.x);
            float y = Random.Range(vector1.y, vector2.y);

            return new Vector2(x, y);
        }

        public static Vector3 RandomRangeVector3(this Vector3 randomVector, Vector3 vector1, Vector3 vector2)
        {
            float x = Random.Range(vector1.x, vector2.x);
            float y = Random.Range(vector1.y, vector2.y);
            float z = Random.Range(vector1.z, vector2.z);

            return new Vector3(x, y, z);
        }
    }
}
