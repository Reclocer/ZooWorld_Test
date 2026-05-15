using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace SUBS.Core
{
    [Serializable]
    public struct LogColor
    {
        [HorizontalGroup]
        [HideLabel] public CustomColorName Name;

        [HorizontalGroup]
        [HideLabel] public Color Color;
    }
}
