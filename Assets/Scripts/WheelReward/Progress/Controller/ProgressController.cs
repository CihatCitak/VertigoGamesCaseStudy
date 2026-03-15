using System;
using Zenject;
using WheelReward.Signals;
using WheelReward.Progress.Model;
using WheelReward.Progress.Interface;
using WheelReward.Reward.Interface;

namespace WheelReward.Progress.Controller
{
    public class ProgressController : IProgressController, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IProgressBar _progressBar;
        private readonly ProgressBarConfig _progressBarConfig;
        private readonly IRewardController _rewardController;

        private int _progress = 1;
        private StageType _currentStageType;

        public int CurrentStage => _progress;
        public int MaxProgress => _progressBarConfig.FinalStage;

        public ProgressController(SignalBus signalBus, IRewardController rewardController, IProgressBar progressBar,
            ProgressBarConfig progressBarConfig)
        {
            _signalBus = signalBus;
            _rewardController = rewardController;
            _progressBar = progressBar;
            _progressBarConfig = progressBarConfig;
            
            _signalBus.Subscribe<OnSpinEnd>(OnSpinEnd);
            _signalBus.Subscribe<OnSpinRestart>(OnSpinRestart);
            _currentStageType = GetStageType(_progress);
        }

        public StageType GetStageType(int stage)
        {
            if (stage == _progressBarConfig.FinalStage) return StageType.Final;
            if (stage == 1 || stage % _progressBarConfig.MilestoneInterval == 0) return StageType.Safe;
            return StageType.Normal;
        }

        private void OnSpinEnd()
        {
            _progress++;
            _progressBar.SetProgress(_progress);

            if (_progress > MaxProgress)
            {
                _rewardController.TakeRewards();
                return;
            }

            var newType = GetStageType(_progress);
            if (newType == _currentStageType) return;
            _currentStageType = newType;
            _signalBus.Fire(new OnSpinTypeChange(newType));
        }

        private void OnSpinRestart()
        {
            _progress = 1;
            _currentStageType = GetStageType(_progress);
            _progressBar.SetProgress(_progress);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinEnd>(OnSpinEnd);
            _signalBus.TryUnsubscribe<OnSpinRestart>(OnSpinRestart);
        }
    }
}