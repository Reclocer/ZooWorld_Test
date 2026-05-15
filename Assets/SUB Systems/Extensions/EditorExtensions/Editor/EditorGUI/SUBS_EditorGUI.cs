using UnityEditor;
using UnityEngine;

namespace SUBS.Core.EditorExtensions
{
    public static class SUBS_EditorGUI
    {
        public static void ErrorMessage(Rect position, string message)
        {
            EditorGUI.LabelField(position, EditorGUIContent.ErrorMessageWithIcon(message), EditorGUIStyles.Error);
        }

        public static void ErrorMessage(string message)
        {
            EditorGUILayout.LabelField(EditorGUIContent.ErrorMessageWithIcon(message), EditorGUIStyles.Error);
        }
    }
}
