using UnityEditor;
using UnityEngine;

namespace SUBS.Core.EditorExtensions
{
    public class EditorGUIContent
    {
        public static GUIContent NewGameButtonIcon;
        public static GUIContent NewGameNoTutorialButtonIcon;
        //public static GUIContent LoadGameButtonIcon;
        public static GUIContent RestartGameButtonIcon;
        public static GUIContent PauseButtonIcon;
        public static GUIContent SmallErrorIcon;

        static EditorGUIContent()
        {
            NewGameButtonIcon = GetContent("NewGame"); 
            NewGameNoTutorialButtonIcon = GetContent("NewGameNoTutorial");
            //LoadGameButtonIcon = GetContent("Load");
            RestartGameButtonIcon = GetContent("Restart");
            PauseButtonIcon = GetContent("PauseButton");
            SmallErrorIcon = GetContent("console.erroricon.sml");
        }

        public static GUIContent ErrorMessageWithIcon(string message)
        {
            return new GUIContent(SmallErrorIcon) { text = message };
        }

        public static GUIContent GetContent(string contentName)
        {
            return EditorGUIUtility.IconContent(contentName);
        }
    }
}
