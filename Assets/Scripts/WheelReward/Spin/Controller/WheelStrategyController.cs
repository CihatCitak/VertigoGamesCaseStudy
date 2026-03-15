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
        private WheelRewardConfig _activeRewardConfig;
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
            ActivateForStage(_progressController.GetStageType(_progressController.CurrentStage));
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinTypeChange>(OnSpinTypeChange);
        }

        #region IWheelStrategy

        public IWheelView GetCurrentWheelView() => _activeWheelView;

        public WheelRewardConfig GetCurrentRewardConfig() => _activeRewardConfig;

        #endregion

        #region Signal Handling

        private void OnSpinTypeChange(OnSpinTypeChange signal)
        {
            ActivateForStage(signal.StageType);
        }

        #endregion

        #region Stage Logic

        private void ActivateForStage(StageType stageType)
        {
            if(_currentStage  == stageType) return;
            _currentStage  = stageType;
            
            _activeWheelView = stageType == StageType.Final ? _goldWheelView
                             : stageType == StageType.Safe  ? _silverWheelView
                             : _bronzeWheelView;
            _activeRewardConfig = stageType == StageType.Final ? _goldConfig
                                : stageType == StageType.Safe  ? _silverConfig
                                : _bronzeConfig;

            _bronzeWheelView.gameObject.SetActive(false);
            _silverWheelView.gameObject.SetActive(false);
            _goldWheelView.gameObject.SetActive(false);

            _activeWheelView.gameObject.SetActive(true);
            _activeWheelView.PlayShowTween();
        }

        #endregion
    }
}
