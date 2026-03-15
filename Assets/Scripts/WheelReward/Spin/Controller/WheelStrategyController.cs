using System;
using Zenject;
using WheelReward.Signals;
using WheelReward.Spin.View;
using WheelReward.Spin.Model;
using WheelReward.Spin.Interface;
using WheelReward.Progress.Model;
using WheelReward.Progress.Interface;

namespace WheelReward.Spin.Controller
{
    public class WheelStrategyController : IWheelStrategy, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IProgressController _progressController;
        private readonly WheelView _bronzeWheelView;
        private readonly WheelView _silverWheelView;
        private readonly WheelView _goldWheelView;
        private readonly WheelRewardConfig _bronzeConfig;
        private readonly WheelRewardConfig _silverConfig;
        private readonly WheelRewardConfig _goldConfig;

        private WheelView _activeWheelView;
        private StageType _currentStage = StageType.Final;

        public WheelStrategyController(
            SignalBus signalBus,
            IProgressController progressController,
            WheelView bronzeWheelView,
            WheelView silverWheelView,
            WheelView goldWheelView,
            WheelRewardConfig bronzeConfig,
            WheelRewardConfig silverConfig,
            WheelRewardConfig goldConfig)
        {
            _signalBus = signalBus;
            _progressController = progressController;
            _bronzeWheelView = bronzeWheelView;
            _silverWheelView = silverWheelView;
            _goldWheelView = goldWheelView;
            _bronzeConfig = bronzeConfig;
            _silverConfig = silverConfig;
            _goldConfig = goldConfig;

            _signalBus.Subscribe<OnSpinTypeChange>(OnSpinTypeChange);
            _signalBus.Subscribe<OnSpinRestart>(OnSpinRestart);
            
            ActivateForStage(_progressController.GetStageType(_progressController.CurrentStage));
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinTypeChange>(OnSpinTypeChange);
            _signalBus.TryUnsubscribe<OnSpinRestart>(OnSpinRestart);
        }

        #region IWheelStrategy

        public IWheelView GetCurrentWheelView() => _activeWheelView;

        #endregion

        #region Signal Handling

        private void OnSpinTypeChange(OnSpinTypeChange signal)
        {
            ActivateForStage(signal.StageType);
        }

        private void OnSpinRestart()
        {
            _currentStage = StageType.Final;
            ActivateForStage(_progressController.GetStageType(1));
        }

        #endregion

        #region Stage Logic

        private void ActivateForStage(StageType stageType)
        {
            if (_currentStage == stageType) return;
            _currentStage = stageType;

            var config = stageType == StageType.Final ? _goldConfig
                       : stageType == StageType.Safe  ? _silverConfig
                       : _bronzeConfig;

            _activeWheelView = stageType == StageType.Final ? _goldWheelView
                             : stageType == StageType.Safe  ? _silverWheelView
                             : _bronzeWheelView;

            _bronzeWheelView.gameObject.SetActive(false);
            _silverWheelView.gameObject.SetActive(false);
            _goldWheelView.gameObject.SetActive(false);

            _activeWheelView.gameObject.SetActive(true);
            _activeWheelView.SetupRewards(
                config.GetRandomRewards(),
                _progressController.CurrentStage,
                _progressController.MaxProgress);
            _activeWheelView.PlayShowTween();
        }

        #endregion
    }
}
