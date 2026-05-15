using UnityEngine;
using DG.Tweening;

namespace SUBS.Core
{
    public static class EaseFunctionsFloat
    {
        #region DoTween Ease arg1
        public static float DoEase(Ease ease, float t)
        {
            switch(ease)
            {
                case Ease.Linear:
                    return Linear(t);

                case Ease.InQuad:
                    return EaseInQuad(t);
                case Ease.OutQuad:
                    return EaseOutQuad(t);
                case Ease.InOutQuad:
                    return EaseInOutQuad(t);

                case Ease.InCubic:
                    return EaseInCubic(t);
                case Ease.OutCubic:
                    return EaseOutCubic(t);
                case Ease.InOutCubic:
                    return EaseInOutCubic(t);

                case Ease.InQuart:
                    return EaseInQuart(t);
                case Ease.OutQuart:
                    return EaseOutQuart(t);
                case Ease.InOutQuart:
                    return EaseInOutQuart(t);

                case Ease.InQuint:
                    return EaseInQuint(t);
                case Ease.OutQuint:
                    return EaseOutQuint(t);
                case Ease.InOutQuint:
                    return EaseInOutQuint(t);

                case Ease.InSine:
                    return EaseInSine(t);
                case Ease.OutSine:
                    return EaseOutSine(t);
                case Ease.InOutSine:
                    return EaseInOutSine(t);

                case Ease.InExpo:
                    return EaseInExpo(t);
                case Ease.OutExpo:
                    return EaseOutExpo(t);
                case Ease.InOutExpo:
                    return EaseInOutExpo(t);

                case Ease.InCirc:
                    return EaseInCirc(t);
                case Ease.OutCirc:
                    return EaseOutCirc(t);
                case Ease.InOutCirc:
                    return EaseInOutCirc(t);

                case Ease.InBack:
                    return EaseInBack(t);
                case Ease.OutBack:
                    return EaseOutBack(t);
                case Ease.InOutBack:
                    return EaseInOutBack(t);

                case Ease.InBounce:
                    return EaseInBounce(t);
                case Ease.OutBounce:
                    return EaseOutBounce(t);
                case Ease.InOutBounce:
                    return EaseInOutBounce(t);

                case Ease.InElastic:
                    return EaseInElastic(t);
                case Ease.OutElastic:
                    return EaseOutElastic(t);
                case Ease.InOutElastic:
                    return EaseInOutElastic(t);

                default:
                    return t;
            }
        }

        public static float Linear(float t)
        {
            return t;
        }

        public static float EaseInQuad(float t)
        {
            return t * t;
        }

        public static float EaseOutQuad(float t)
        {
            return t * (2f - t);
        }

        public static float EaseInOutQuad(float t)
        {
            if(t < 0.5f)
            {
                return 2f * Mathf.Pow(t, 2);
            }
            else
            {
                return -1f + (4f - 2f * t) * t;
            }
        }

        public static float EaseInCubic(float t)
        {
            return Mathf.Pow(t,3);
        }

        public static float EaseOutCubic(float t)
        {
            t -= 1f;
            return 1f + Mathf.Pow(t, 3);
        }

        public static float EaseInOutCubic(float t)
        {
            if(t < 0.5f)
            {
                return 4f * Mathf.Pow(t, 3);
            }
            else
            {
                float f = 2f * t - 2f;
                return 0.5f * Mathf.Pow(f, 3) + 1f;
            }
        }

        public static float EaseInQuart(float t)
        {
            return Mathf.Pow(t, 4);
        }

        public static float EaseOutQuart(float t)
        {
            t -= 1f;
            return 1f - Mathf.Pow(t, 4);
        }

        public static float EaseInOutQuart(float t)
        {
            if(t < 0.5f)
            {
                return 8f * Mathf.Pow(t, 4);
            }
            else
            {
                t -= 1f;
                return 1f - 8f * Mathf.Pow(t, 4);
            }
        }

        public static float EaseInQuint(float t)
        {
            return Mathf.Pow(t, 5);
        }

        public static float EaseOutQuint(float t)
        {
            t -= 1f;
            return 1f + Mathf.Pow(t, 5);
        }

        public static float EaseInOutQuint(float t)
        {
            if(t < 0.5f)
            {
                return 16f * Mathf.Pow(t, 5);
            }
            else
            {
                t -= 1f;
                return 1f + 16f * Mathf.Pow(t, 5);
            }
        }

        public static float EaseInSine(float t)
        {
            return 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
        }

        public static float EaseOutSine(float t)
        {
            return Mathf.Sin(t * Mathf.PI * 0.5f);
        }

        public static float EaseInOutSine(float t)
        {
            return -(Mathf.Cos(Mathf.PI * t) - 1f) / 2f;
        }

        public static float EaseInExpo(float t)
        {
            if(t == 0f)
            {
                return 0f;
            }
            return Mathf.Pow(2f, 10f * (t - 1f));
        }

        public static float EaseOutExpo(float t)
        {
            if(t == 1f)
            {
                return 1f;
            }
            return 1f - Mathf.Pow(2f, -10f * t);
        }

        public static float EaseInOutExpo(float t)
        {
            if(t == 0f)
            {
                return 0f;
            }
            if(t == 1f)
            {
                return 1f;
            }
            if(t < 0.5f)
            {
                return Mathf.Pow(2f, 20f * t - 10f) / 2f;
            }
            else
            {
                return (2f - Mathf.Pow(2f, -20f * t + 10f)) / 2f;
            }
        }

        public static float EaseInCirc(float t)
        {
            return 1f - Mathf.Sqrt(1f - t * t);
        }

        public static float EaseOutCirc(float t)
        {
            t -= 1f;
            return Mathf.Sqrt(1f - t * t);
        }

        public static float EaseInOutCirc(float t)
        {
            if(t < 0.5f)
            {
                return (1f - Mathf.Sqrt(1f - 4f * Mathf.Pow(t, 2))) / 2f;
            }
            else
            {
                float f = 2f * t - 2f;
                return (Mathf.Sqrt(1f - Mathf.Pow(f, 2)) + 1f) / 2f;
            }
        }

        public static float EaseInBack(float t)
        {
            float c1 = 1.70158f;
            return c1 * Mathf.Pow(t,3) - c1 * Mathf.Pow(t, 2);
        }

        public static float EaseOutBack(float t)
        {
            float c1 = 1.70158f;
            t -= 1f;
            return 1f + c1 * Mathf.Pow(t, 3) + c1 * Mathf.Pow(t, 2);
        }

        public static float EaseInOutBack(float t)
        {
            float c1 = 1.70158f;
            float c2 = c1 * 1.525f;

            if(t < 0.5f)
            {
                float f = 2f * t;
                return (Mathf.Pow(f, 2) * ((c2 + 1f) * f - c2)) / 2f;
            }
            else
            {
                float f = 2f * t - 2f;
                return (Mathf.Pow(f, 2) * ((c2 + 1f) * f + c2) + 2f) / 2f;
            }
        }

        public static float EaseOutBounce(float t)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;

            if(t < 1f / d1)
            {
                return n1 * t * t;
            }
            else if(t < 2f / d1)
            {
                t -= 1.5f / d1;
                return n1 * t * t + 0.75f;
            }
            else if(t < 2.5f / d1)
            {
                t -= 2.25f / d1;
                return n1 * t * t + 0.9375f;
            }
            else
            {
                t -= 2.625f / d1;
                return n1 * t * t + 0.984375f;
            }
        }

        public static float EaseInBounce(float t)
        {
            return 1f - EaseOutBounce(1f - t);
        }

        public static float EaseInOutBounce(float t)
        {
            if(t < 0.5f)
            {
                return (1f - EaseOutBounce(1f - 2f * t)) / 2f;
            }
            else
            {
                return (1f + EaseOutBounce(2f * t - 1f)) / 2f;
            }
        }

        public static float EaseInElastic(float t)
        {
            if(t == 0f || t == 1f)
                return t;

            float p = 0.3f;
            float s = p / 4f;
            t -= 1f;
            return -Mathf.Pow(2f, 10f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p);
        }

        public static float EaseOutElastic(float t)
        {
            if(t == 0f || t == 1f)
                return t;

            float p = 0.3f;
            float s = p / 4f;
            return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p) + 1f;
        }

        public static float EaseInOutElastic(float t)
        {
            if(t == 0f || t == 1f)
                return t;

            t *= 2f;
            float p = 0.45f;
            float s = p / 4f;

            if(t < 1f)
            {
                t -= 1f;
                return -0.5f * Mathf.Pow(2f, 10f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p);
            }
            else
            {
                t -= 1f;
                return Mathf.Pow(2f, -10f * t) * Mathf.Sin((t - s) * (2f * Mathf.PI) / p) * 0.5f + 1f;
            }
        }
        #endregion DoTween Ease arg1

        #region DoTween other
        public static float EaseFlash(float t, int flashes = 3)
        {
            float sine = Mathf.Sin(t * Mathf.PI * flashes);
            float decay = 1f - t;
            return sine * decay;
        }

        public static float EaseInFlash(float t, int flashes = 3)
        {
            float sine = Mathf.Sin(t * Mathf.PI * flashes);
            float decay = Mathf.Pow(t, 2f);
            return sine * decay;
        }

        public static float EaseOutFlash(float t, int flashes = 3)
        {
            float sine = Mathf.Sin(t * Mathf.PI * flashes);
            float decay = Mathf.Pow(1f - t, 2f);
            return sine * decay;
        }

        public static float EaseInOutFlash(float t, int flashes = 3)
        {
            float sine = Mathf.Sin(t * Mathf.PI * flashes);
            float decay = 1f - Mathf.Abs(2f * t - 1f);
            return sine * decay;
        }
        #endregion DoTween other

        #region CustomEase arg1
        public static float DoCustomEase(CustomEase ease, float t)
        {
            switch(ease)
            {
                default:
                    return t;
            }
        }
        #endregion CustomEase arg1
    }
}