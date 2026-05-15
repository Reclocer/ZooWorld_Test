using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UniRx;
using TMPro;

namespace SUBS.Packages.ForTesting
{
    public class ForFastTesting : MonoBehaviour
    {
        [OnValueChanged(nameof(OnTestBuildChange))]
        [SerializeField] private bool _isTestBuild = true;
        public bool IsTestBuild => _isTestBuild;

        [Space]
        [SerializeField] private TextMeshProUGUI _testBuildLabel;
        [SerializeField] private Button _addMoneyBtn;
        [SerializeField] private GameObject _speedBtnsRoot;

        //[EnableIf(nameof(_isPlayMode))]
        //[AllowNesting]
        //[SerializeField] private bool _showCameraAnimation = true;
        //private bool _showCameraAnimation = true;
        //public bool ShowCameraAnimation => _showCameraAnimation;

        private bool _isPlayMode => Application.isPlaying;

        [EnableIf(nameof(_isPlayMode))]        
        //[Range(0.1f, 5)]
        private ReactiveProperty<float> _gameSpeed = new ReactiveProperty<float>(1);
        public ReactiveProperty<float> GameSpeed => _gameSpeed;

        [Space]
        //[SerializeField] private List<TaskForPlayer> _playerTasks;
        private List<TaskForPlayer> _playerTasks = new List<TaskForPlayer>();        

        private void Awake()
        {         
            //_showCameraAnimation = true;
            OnTestBuildChange();

            _gameSpeed.Value = 1;
            _gameSpeed.Subscribe(speed => Time.timeScale = speed);
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.KeypadPlus) 
             || Input.GetKeyDown(KeyCode.Plus) 
             || Input.GetKeyDown(KeyCode.UpArrow))
            {
                UpSpeed();
            }
            else if (Input.GetKeyDown(KeyCode.KeypadMinus) 
                  || Input.GetKeyDown(KeyCode.Minus)
                  || Input.GetKeyDown(KeyCode.DownArrow))
            {
                DownSpeed();
            }
#endif
        }

        #region Speed
        public void UpSpeed()
        {
            float lastValue = _gameSpeed.Value;

            if (lastValue == 0.6f)
            {
                _gameSpeed.Value = 1;
            }
            else
            {
                lastValue += 0.5f;

                if (lastValue > 10)
                {
                    _gameSpeed.Value = 10;
                }
                else
                {
                    _gameSpeed.Value = lastValue;
                }
            }

            Debug.Log($"Simulation speed {_gameSpeed.Value}");
        }

        public void DownSpeed()
        {
            float value = _gameSpeed.Value - 0.5f;

            if (value < 0.1f)
            {
                _gameSpeed.Value = 0.1f;
            }
            else
            {
                _gameSpeed.Value = value;
            }

            Debug.Log($"Simulation speed {_gameSpeed.Value}");
        }

        public void SetToNormalSpeed()
        {
            if (_gameSpeed.Value != 1)
            {
                Color color = Color.green;
                _gameSpeed.Value = 1;
                Debug.Log($"<color=#00FF00>Simulation speed {_gameSpeed.Value}</color>");
            }
        }
        #endregion Speed

        #region Tasks
        public TaskForPlayer CreateTask(string taskName, Action taskCallBack)
        {
            TaskForPlayer task = new TaskForPlayer(taskName, taskCallBack);

            //StartCoroutine(this.DoAfterSeconds(() =>
            //{
            _playerTasks.Add(task);
            //}, 1));

            return task;
        }

        public void RemoveTask(TaskForPlayer task)
        {
            _playerTasks.Remove(task);
        }

        public void ClearTaskList()
        {
            _playerTasks.Clear();
        }
        #endregion Tasks

        public void AddMoney()
        {
            //Score.Instance.AddScoreValue(100);
        }

        private void OnTestBuildChange()
        {
            if (_isTestBuild)
            {
                //_testBuildLabel.gameObject.SetActive(true);
                _addMoneyBtn.gameObject.SetActive(true);
                _speedBtnsRoot.gameObject.SetActive(true);
            }
            else
            {
                _testBuildLabel.gameObject.SetActive(false);
                _addMoneyBtn.gameObject.SetActive(false);
                _speedBtnsRoot.gameObject.SetActive(false);
            }
        }

        //[Button] //OnPlayMode
        //public void PrintMessengerEvents()
        //{
        //    Messenger.Messenger.PrintEvents();
        //}
    }
}
