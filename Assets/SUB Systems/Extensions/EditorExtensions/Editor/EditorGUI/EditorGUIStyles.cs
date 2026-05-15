using UnityEditor;
using UnityEngine;

namespace SUBS.Core.EditorExtensions
{
    public class EditorGUIStyles
    {
        private static GUIStyle _centerHeader;
        public static GUIStyle CenterHeader => _centerHeader;

        private static GUIStyle _centerBoldHeader;
        public static GUIStyle CenterBoldHeader => _centerBoldHeader;

        private static GUIStyle _noMargin;
        public static GUIStyle NoMargin => _noMargin;

        private static GUIStyle _assetLabel;
        public static GUIStyle AssetLabel => _assetLabel;

        public static GUIStyle SearchTextField;
        public static GUIStyle SearchTextCancelButtonEmpty;
        public static GUIStyle SearchTextCancelButton;
        public static GUIStyle LeftBoldHeader;
        public static GUIStyle Right;
        public static GUIStyle Error;
        public static GUIStyle Label;
        public static GUIStyle TextArea;

        static EditorGUIStyles()
        {
            _centerHeader = new GUIStyle(EditorStyles.largeLabel) {alignment = TextAnchor.MiddleCenter};
            _centerBoldHeader = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.MiddleCenter};
            _noMargin = new GUIStyle(EditorStyles.miniLabel) {margin = new RectOffset(0, 0, 0, 0)};
            _assetLabel = GetStyle("AssetLabel"); 
            SearchTextField = GetStyle("SearchTextField");
            SearchTextCancelButtonEmpty = GetStyle("SearchCancelButtonEmpty");
            SearchTextCancelButton = GetStyle("SearchCancelButton");
            LeftBoldHeader = new GUIStyle(EditorStyles.boldLabel) {alignment = TextAnchor.LowerLeft};
            Right = new GUIStyle() {alignment = TextAnchor.LowerRight};

            Error = new GUIStyle(EditorStyles.label)
            {
                normal = new GUIStyleState()
                {
                    //background = TextrureCreator.SolidColorTexture(MyColors.errorAutoCompleteColor),
                    textColor = Color.white
                },
                wordWrap = true
            };

            Label = new GUIStyle(EditorStyles.label)
            {
                normal = new GUIStyleState()
                {
                    //background = TextrureCreator.SolidColorTexture(MyColors.reorderableListColor),
                    textColor = EditorColors.UnityNotEditableTextColor
                },
                wordWrap = true
            };
            
            TextArea = new GUIStyle(EditorStyles.textArea)
            {
                normal = new GUIStyleState()
                {
                    //background = TextrureCreator.SolidColorTexture(MyColors.errorAutoCompleteColor),
                    textColor = Color.white
                },
                wordWrap = true
            };
        }

        public static GUIStyle GetStyle(string styleName)
        {
            GUIStyle s = GUI.skin.FindStyle(styleName);

            if (s == null)
            {
                s = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector).FindStyle(styleName);
            }

            if (s == null)
            {
                Debug.LogError("Missing built-in guistyle " + styleName);
            }

            return s;
        }
    }
}