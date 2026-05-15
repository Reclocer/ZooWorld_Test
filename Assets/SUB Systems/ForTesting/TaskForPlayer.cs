using System;
using UnityEngine;
using Sirenix.OdinInspector;
using SUBS.Core;
using Cysharp.Threading.Tasks;
using Zenject;

namespace SUBS.Packages.ForTesting
{
    [Serializable]
    public class TaskForPlayer
    {
        [InlineButton("Upgrade")]
        //[ReadOnlyAttribute]
        [SerializeField] private string _taskName = "DefaultName";

        private Action _taskCallBack;
        private bool _upgraded;
        private ForFastTesting _forFastTesting;

        public TaskForPlayer(ForFastTesting forFastTesting)
        {
            _forFastTesting = forFastTesting;
        }

        private void Upgrade()
        {
            if (_upgraded)
                return;

            _upgraded = true;
            _taskCallBack.Invoke();
            _forFastTesting.RemoveTask(this);

            //RemoveTask();
        }

        //private async UniTaskVoid RemoveTask()
        //{
        //    await UniTask.Delay(TimeSpan.FromSeconds(1));
        //    ForFastTesting.Instance.RemoveTask(this);
        //}

        public TaskForPlayer(string taskName, Action taskCallBack)
        {
            _taskName = taskName;
            _taskCallBack = taskCallBack;
        }
    }
}