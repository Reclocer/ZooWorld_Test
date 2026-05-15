using UnityEngine;

namespace SUBS.Core
{
    public static class UIExtensions
    {
        public static RectTransform GetRectTransform_OfComponent(this Component component)
        {
            return component.transform as RectTransform;
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }
}