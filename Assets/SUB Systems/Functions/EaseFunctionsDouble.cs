using DG.Tweening;
using System;
using UnityEngine;

namespace SUBS.Core
{
    public static class EaseFunctionsDouble
    {
        #region DoTween Ease arg1
        public static double DoEase(Ease ease, double t)
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

        public static double Linear(double t)
        {
            return t;
        }

        public static double EaseInQuad(double t)
        {
            return t * t;
        }

        public static double EaseOutQuad(double t)
        {
            return t * (2f - t);
        }

        public static double EaseInOutQuad(double t)
        {
            if(t < 0.5f)
            {
                return 2f * Math.Pow(t, 2);
            }
            else
            {
                return -1f + (4f - 2f * t) * t;
            }
        }

        public static double EaseInCubic(double t)
        {
            return Math.Pow(t, 3);
        }

        public static double EaseOutCubic(double t)
        {
            t -= 1f;
            return 1f + Math.Pow(t, 3);
        }

        public static double EaseInOutCubic(double t)
        {
            if(t < 0.5f)
            {
                return 4f * Math.Pow(t, 3);
            }
            else
            {
                double f = 2f * t - 2f;
                return 0.5f * Math.Pow(f, 3) + 1f;
            }
        }

        public static double EaseInQuart(double t)
        {
            return Math.Pow(t, 4);
        }

        public static double EaseOutQuart(double t)
        {
            t -= 1f;
            return 1f - Math.Pow(t, 4);
        }

        public static double EaseInOutQuart(double t)
        {
            if(t < 0.5f)
            {
                return 8f * Math.Pow(t, 4);
            }
            else
            {
                t -= 1f;
                return 1f - 8f * Math.Pow(t, 4);
            }
        }

        public static double EaseInQuint(double t)
        {
            return Math.Pow(t, 5);
        }

        public static double EaseOutQuint(double t)
        {
            t -= 1f;
            return 1f + Math.Pow(t, 5);
        }

        public static double EaseInOutQuint(double t)
        {
            if(t < 0.5f)
            {
                return 16f * Math.Pow(t, 5);
            }
            else
            {
                t -= 1f;
                return 1f + 16f * Math.Pow(t, 5);
            }
        }

        public static double EaseInSine(double t)
        {
            return 1f - Math.Cos(t * Mathf.PI * 0.5f);
        }

        public static double EaseOutSine(double t)
        {
            return Math.Sin(t * Mathf.PI * 0.5f);
        }

        public static double EaseInOutSine(double t)
        {
            return -(Math.Cos(Mathf.PI * t) - 1f) / 2f;
        }

        public static double EaseInExpo(double t)
        {
            if(t == 0f)
            {
                return 0f;
            }
            return Math.Pow(2f, 10f * (t - 1f));
        }

        public static double EaseOutExpo(double t)
        {
            if(t == 1f)
            {
                return 1f;
            }
            return 1f - Math.Pow(2f, -10f * t);
        }

        public static double EaseInOutExpo(double t)
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
                return Math.Pow(2f, 20f * t - 10f) / 2f;
            }
            else
            {
                return (2f - Math.Pow(2f, -20f * t + 10f)) / 2f;
            }
        }

        public static double EaseInCirc(double t)
        {
            return 1f - Math.Sqrt(1f - t * t);
        }

        public static double EaseOutCirc(double t)
        {
            t -= 1f;
            return Math.Sqrt(1f - t * t);
        }

        public static double EaseInOutCirc(double t)
        {
            if(t < 0.5f)
            {
                return (1f - Math.Sqrt(1f - 4f * Math.Pow(t, 2))) / 2f;
            }
            else
            {
                double f = 2f * t - 2f;
                return (Math.Sqrt(1f - Math.Pow(f, 2)) + 1f) / 2f;
            }
        }

        public static double EaseInBack(double t)
        {
            float c1 = 1.70158f;
            return c1 * Math.Pow(t, 3) - c1 * Math.Pow(t, 2);
        }

        public static double EaseOutBack(double t)
        {
            float c1 = 1.70158f;
            t -= 1f;
            return 1f + c1 * Math.Pow(t, 3) + c1 * Math.Pow(t, 2);
        }

        public static double EaseInOutBack(double t)
        {
            float c1 = 1.70158f;
            float c2 = c1 * 1.525f;

            if(t < 0.5f)
            {
                double f = 2f * t;
                return (Math.Pow(f, 2) * ((c2 + 1f) * f - c2)) / 2f;
            }
            else
            {
                double f = 2f * t - 2f;
                return (Math.Pow(f, 2) * ((c2 + 1f) * f + c2) + 2f) / 2f;
            }
        }

        public static double EaseOutBounce(double t)
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

        public static double EaseInBounce(double t)
        {
            return 1f - EaseOutBounce(1f - t);
        }

        public static double EaseInOutBounce(double t)
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

        public static double EaseInElastic(double t)
        {
            if(t == 0f || t == 1f)
                return t;

            float p = 0.3f;
            float s = p / 4f;
            t -= 1f;
            return -Math.Pow(2f, 10f * t) * Math.Sin((t - s) * (2f * Mathf.PI) / p);
        }

        public static double EaseOutElastic(double t)
        {
            if(t == 0f || t == 1f)
                return t;

            float p = 0.3f;
            float s = p / 4f;
            return Math.Pow(2f, -10f * t) * Math.Sin((t - s) * (2f * Mathf.PI) / p) + 1f;
        }

        public static double EaseInOutElastic(double t)
        {
            if(t == 0f || t == 1f)
                return t;

            t *= 2f;
            float p = 0.45f;
            float s = p / 4f;

            if(t < 1f)
            {
                t -= 1f;
                return -0.5f * Math.Pow(2f, 10f * t) * Math.Sin((t - s) * (2f * Mathf.PI) / p);
            }
            else
            {
                t -= 1f;
                return Math.Pow(2f, -10f * t) * Math.Sin((t - s) * (2f * Mathf.PI) / p) * 0.5f + 1f;
            }
        }
        #endregion DoTween Ease arg1

        #region DoTween other
        public static double EaseFlash(double t, int flashes = 3)
        {
            double sine = Math.Sin(t * Mathf.PI * flashes);
            double decay = 1f - t;
            return sine * decay;
        }

        public static double EaseInFlash(double t, int flashes = 3)
        {
            double sine = Math.Sin(t * Mathf.PI * flashes);
            double decay = Math.Pow(t, 2f);
            return sine * decay;
        }

        public static double EaseOutFlash(double t, int flashes = 3)
        {
            double sine = Math.Sin(t * Mathf.PI * flashes);
            double decay = Math.Pow(1f - t, 2f);
            return sine * decay;
        }

        public static double EaseInOutFlash(double t, int flashes = 3)
        {
            double sine = Math.Sin(t * Mathf.PI * flashes);
            double decay = 1f - Math.Abs(2f * t - 1f);
            return sine * decay;
        }
        #endregion DoTween other

        #region CustomEase arg1
        public static double DoCustomEase(CustomEase ease, double t)
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