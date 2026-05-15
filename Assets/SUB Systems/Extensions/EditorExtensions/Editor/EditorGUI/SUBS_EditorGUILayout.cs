using UnityEditor;
using UnityEngine;

namespace SUBS.Core.EditorExtensions
{
    public static class SUBS_EditorGUILayout
    {
        static Object previousSelected; 
  
        public static void DrawSeparatorLine()
        {
            EditorGUILayout.LabelField(GUIContent.none, GUI.skin.horizontalSlider);
        }
        
        public static void SmallSpace()
        {
            GUILayoutUtility.GetRect(1f, 1f, GUILayout.MaxWidth(1f));
        }

        public static void ErrorMessage(string message)
        {
            EditorGUILayout.LabelField(EditorGUIContent.ErrorMessageWithIcon(message), EditorGUIStyles.Error);
        }

        public static void DrawBackButton()
        {
            if (previousSelected == null)
                return;
            
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (GUILayout.Button("<<", EditorStyles.miniLabel, GUILayout.Width(18)))
            {
                Object currentSelected = Selection.activeObject;
                string path = AssetDatabase.GetAssetPath(previousSelected);
                Selection.activeObject = AssetDatabase.LoadMainAssetAtPath(path);
                previousSelected = currentSelected;
            }

            EditorGUILayout.EndHorizontal();
        }

        public static void SetPreviousSelectedObject(Object obj)
        {
            previousSelected = obj;
        }
    }
}
