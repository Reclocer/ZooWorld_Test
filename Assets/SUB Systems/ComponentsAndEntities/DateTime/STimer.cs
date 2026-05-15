using System;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace SUBS.Core
{
    /// <summary>
    /// Use case:
    /// _timer = new STimer(10);
    /// _timer.OnTimeUpdate += UpdateTime;
    /// _timer.OnTimeIsOver += OnTimerFinished;
    /// _timer.Play();
    /// </summary>
    [Serializable]
    public class STimer
    {
        private double _timerTimeSec = 180;

        private double _restTimeSec = 20;
        public double RestTimeSec => _restTimeSec;

        private const float STEP_TIME_SEC = 0.1f;
        private bool _started = false;
        private CancellationTokenSource _timerCancellation;

        public event Action OnTimeIsOver = () => { };
        public event Action OnPauseTimer = () => { };
        public event Action<double,double> OnTimeUpdate = (rest,all) => { };

        #region Constructors
        public STimer()
        {

        }

        public STimer(double timerTimeSec, bool startTimer = false)
        {
            _timerTimeSec = timerTimeSec;
            _restTimeSec = timerTimeSec;

            if(startTimer)
                Play();
        }

        public STimer(double timerTimeSec, double startTimerTimeSec, bool startTimer = false)
        {
            _timerTimeSec = timerTimeSec;
            _restTimeSec = startTimerTimeSec;

            if(startTimer)
                Play();
        }
        #endregion Constructors

        public void Play()
        {
            if(_started)
            {
                SLogger.Log("[Timer] is on play state!");
                return;
            }

            _started = true;
            _timerCancellation?.Cancel();
            _timerCancellation = new();
            UpdateTime().Forget();
        }

        public void Pause()
        {
            _started = false;
            _timerCancellation?.Cancel();
            _timerCancellation = null;
            OnPauseTimer();
        }

        public void Stop()
        {
            _started = false;
            _restTimeSec = 0;

            if(_timerCancellation != null)
            {
                _timerCancellation.Cancel();
                _timerCancellation = null;
            }
        }

        private async UniTaskVoid UpdateTime()
        {
            try
            {
                while (_started && !_timerCancellation.Token.IsCancellationRequested)
                {
                    await UniTask.Delay(
                        TimeSpan.FromSeconds(STEP_TIME_SEC),
                        cancellationToken: _timerCancellation.Token,
                        cancelImmediately: false
                    );

                    _restTimeSec -= STEP_TIME_SEC;

                    if (_restTimeSec <= 0)
                    {
                        _restTimeSec = 0;
                        _started = false;
                        OnTimeIsOver();
                        break;
                    }

                    OnTimeUpdate(_restTimeSec, _timerTimeSec);
                }
            }
            catch (OperationCanceledException)
            {
                //SLogger.Log("[Timer] cancelled");
            }
        }

        public void SetTimerTime(double seconds, bool playTimer = true)
        {
            if(playTimer)
            {
                Stop();
                _timerTimeSec = seconds;
                _restTimeSec = seconds;
                Play();
            }
            else
            {
                _timerTimeSec = seconds;
                _restTimeSec = seconds;
            }
        }

        public void AddTimerTime(double seconds, bool enableTimer = true)
        {
            _started = enableTimer;
            _restTimeSec += seconds;

            if(_restTimeSec > _timerTimeSec)
                _restTimeSec = _timerTimeSec;
        }

        public void Dispose()
        {
            _started = false;

            if(_timerCancellation != null)
            {
                _timerCancellation.Cancel();
                _timerCancellation.Dispose();
            }
        }
    }
}
