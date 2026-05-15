using Sirenix.OdinInspector;
using UnityEngine;
using System.Collections.Generic;

namespace SUBS.Core
{
    [CreateAssetMenu(fileName = nameof(LoggerConfig), menuName = "SUBS/Configs/" + nameof(LoggerConfig))]
    public class LoggerConfig: ScriptableObject
    {
        [HideInInspector] public bool ConfigIsLoaded = false;

        public bool ShowLogsInEditor => LogsTable[0].ShowLog;
        public bool ShowWarningsInEditor => LogsTable[0].ShowWarning;
        public bool ShowErrorsInEditor => LogsTable[0].ShowError;

        public List<LogPriority> Priority = new();

        public bool ShowLogsInBuild => LogsTable[0].ShowLogInBuild;
        public bool ShowWarningsInBuild => LogsTable[0].ShowWarningInBuild;
        public bool ShowErrorsInBuild => LogsTable[0].ShowErrorInBuild;

        [TableList(DrawScrollView = true, MaxScrollViewHeight = 200, MinScrollViewHeight = 100)]
        public List<LogsTableContent> LogsTable = new() {new LogsTableContent()};   
        
        [Space]
        [Tooltip("Create custom color for log text")]
        [SerializeField] private LogColor[] _logColors;
        public LogColor[] LogColors => _logColors;
    }
}