using System;
using System.Collections.Generic;
using UnityEngine;

namespace SUBS.Core
{
    public static class AnimationExtensions
    {
        public static bool ContainsParameter(this Animator animator, string parameter)
        {
            foreach (AnimatorControllerParameter parameter1 in animator.parameters)
            {
                if (parameter1.name == parameter)
                    return true;
            }

            return false;
        }
    }
}
