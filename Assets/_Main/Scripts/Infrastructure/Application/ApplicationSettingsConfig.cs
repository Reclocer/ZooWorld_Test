using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = nameof(ApplicationSettingsConfig), menuName = "SUBS/Configs/" + nameof(ApplicationSettingsConfig))]
public sealed class ApplicationSettingsConfig : ScriptableObject
{
    [InfoBox("Set -1 for max FPS")]
    [SerializeField] private int _targetFrameRate = 60;
    public int TargetFrameRate => _targetFrameRate;

    [SerializeField] private bool _inputMultiTouchEnabled = false;
    public bool InputMultiTouchEnabled => _inputMultiTouchEnabled;

    [SerializeField] private bool _runInBackground = true;
    public bool RunInBackground => _runInBackground;

    [SerializeField] private bool _debugDeveloperConsoleVisible = false;
    public bool DebugDeveloperConsoleVisible => _debugDeveloperConsoleVisible;

    [SerializeField] private bool _skipStartCutscene_InEditor = false;

    [SerializeField] private bool _needUseBaseGameLevel = false;
    public bool NeedUseBaseGameLevel => _needUseBaseGameLevel;

    //[SerializeField, ShowIf(nameof(_needUseBaseGameLevel))] private LevelName _baseLevel;
    //public LevelName BaseLevel => _baseLevel;

    [Header("Screen settings")]
    [SerializeField] private List<ResolutionScreenSize> _resolutionList;
    public List<ResolutionScreenSize> ResolutionList => _resolutionList;

    [SerializeField] private List<FullScreenMode> _screenModesList;

    public List<FullScreenMode> ScreenModes => _screenModesList;

    public FullScreenMode CurrentMode;
    public ResolutionScreenSize CurrentResolution;

    public bool SkipStartCutscene_InEditor
    {
        get
        {
#if UNITY_EDITOR
            return _skipStartCutscene_InEditor;
#else
        return false;
#endif
        }

        private set
        {
        }
    }

    public void Apply()
    {
        Application.targetFrameRate = _targetFrameRate;
        Input.multiTouchEnabled = _inputMultiTouchEnabled;
        Debug.developerConsoleVisible = _debugDeveloperConsoleVisible;

#if UNITY_EDITOR
        Application.runInBackground = _runInBackground;
#endif

        List<ResolutionScreenSize> resList = new();

        resList.Add(new ResolutionScreenSize { Width = 2560, Height = 1440 });
        resList.Add(new ResolutionScreenSize { Width = 1920, Height = 1080 });
        resList.Add(new ResolutionScreenSize { Width = 1280, Height = 720 });
        _resolutionList = resList;
    }

    //public void SetBaseLevel(LevelName level)
    //{
    //    _baseLevel = level;
    //}
}