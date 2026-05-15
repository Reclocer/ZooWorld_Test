using Sirenix.OdinInspector;
using UnityEngine;
using System;

namespace SUBS.Core
{
    [Serializable]
    public class LogsTableContent
    {
        [TableColumnWidth(150, true)]
        [VerticalGroup(GroupID = "TargetPlatform")]
        [HideLabel]
        [SerializeField, ReadOnly] private string TargetPlatform = "Show in editor";

        [TableColumnWidth(40, false)]
        [VerticalGroup("Log")]
        [HideLabel]
        public bool ShowLog = true;

        [TableColumnWidth(60, false)]
        [VerticalGroup("Warning")]
        [HideLabel]
        [GUIColor("#D4AE40")]
        public bool ShowWarning = true;

        [TableColumnWidth(50, false)]
        [VerticalGroup("Error")]
        [HideLabel]
        [GUIColor("red")]
        public bool ShowError = true;

        [VerticalGroup("TargetPlatform")]
        [HideLabel]
        [SerializeField, ReadOnly] private string TargetPlatform2 = "Show in build";

        [VerticalGroup("Log")]
        [HideLabel]
        public bool ShowLogInBuild = true;

        [VerticalGroup("Warning")]
        [HideLabel]
        [GUIColor("#D4AE40")]
        public bool ShowWarningInBuild = true;

        [VerticalGroup("Error")]
        [HideLabel]
        [GUIColor("red")]
        public bool ShowErrorInBuild = true;
    }
}