using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestPanel : MonoBehaviour
{
    [SerializeField, Required] private GameObject _root;
    [SerializeField, Required] private Button _onOffPanelBtn;

    [SerializeField, Required] private Button _restartGameBtn;
    //[SerializeField, Required] private DebugClearSave _clearSave;

    [Space]
    [SerializeField, Required] private Button _addMoneyBtn;
    [SerializeField, Required] private Button _addHardCurrencyBtn;
    [SerializeField, Required] private Button _addMetalBtn;
    [SerializeField, Required] private Button _addRubberBtn;

    [Space]
    [SerializeField, Required] private Button _skipTaskBtn;
    [SerializeField, Required] private TextMeshProUGUI _speedText;
    [SerializeField, Required] private Slider _gameSpeedSlider;

    //private WalletSystem _walletSystem;

    private void Start()
    {
        //_walletSystem = WalletSystem.Instance;

        _onOffPanelBtn.onClick.AddListener(() => _root.SetActive(!_root.activeSelf));
        _restartGameBtn.onClick.AddListener(RestartGame);

        _addMoneyBtn.onClick.AddListener(AddMoney);
        _addHardCurrencyBtn.onClick.AddListener(AddHardCurrency);
        _addMetalBtn.onClick.AddListener(AddMetal);
        _addRubberBtn.onClick.AddListener(AddRubber);

        //QuestSystem questSystem = ServiceLocator.Get<QuestSystem>();
        //_skipTaskBtn.onClick.AddListener(questSystem.DebugCompleteCurrentQuest);

        _gameSpeedSlider.onValueChanged.AddListener((value) =>
        {
            Time.timeScale = value;
            _speedText.text = $"Game speed: {value}";
        });

        _gameSpeedSlider.value = 1f;
    }   

    private void RestartGame()
    {
        //_clearSave.ClearSave();
    }

    #region Add Resource
    private void AddMoney()
    {
        //_walletSystem.AddCurrency("Money",1000);
    }

    private void AddHardCurrency()
    {
        //_walletSystem.AddCurrency("Diamond", 10);
    }

    private void AddMetal()
    {
        //_walletSystem.AddCurrency("Metal", 1000);
    }

    private void AddRubber()
    {
        //_walletSystem.AddCurrency("Tire", 1000);
    }
    #endregion Add Resource
}
