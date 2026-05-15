using UnityEngine;

namespace SUBS.Core.EditorExtensions
{    
    public class EditorColors
    {
        public static Color ReorderableListColor;
        public static Color ErrorAutoCompleteColor;
        public static Color UnityNotEditableTextColor;
        
        static EditorColors()
        {
            ColorUtility.TryParseHtmlString("#E4E5E4FF", out ReorderableListColor);
            ErrorAutoCompleteColor = new Color(1f, 0f, 0f, 0.28f);
            UnityNotEditableTextColor = new Color(0.9f, 0.0f, 0.0f);
        }
    }
}