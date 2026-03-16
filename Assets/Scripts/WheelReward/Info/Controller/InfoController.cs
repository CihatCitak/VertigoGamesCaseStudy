using System;
using Zenject;
using WheelReward.Signals;
using WheelReward.Info.Interface;
using WheelReward.Progress.Model;
using WheelReward.Progress.Interface;

namespace WheelReward.Info.Controller
{
    public class InfoController : IInfoController, IDisposable
    {
        private readonly IInfoView _infoView;
        private readonly SignalBus _signalBus;
        private readonly IProgressController _progressController;

        public InfoController(SignalBus signalBus, IProgressController progressController, IInfoView infoView)
        {
            _infoView = infoView;
            _signalBus = signalBus;
            _progressController = progressController;

            _signalBus.Subscribe<OnSpinEnd>(UpdateInfo);
            _signalBus.Subscribe<OnSpinRestart>(UpdateInfo);

            UpdateInfo();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinEnd>(UpdateInfo);
            _signalBus.TryUnsubscribe<OnSpinRestart>(UpdateInfo);
        }

        private void UpdateInfo()
        {
            _infoView.SetNextSafeStage(GetNextSafeStage(_progressController.CurrentStage));
            _infoView.SetFinalStage(_progressController.MaxProgress);
        }

        private int GetNextSafeStage(int currentStage)
        {
            for (var stage = currentStage + 1; stage <= _progressController.MaxProgress; stage++)
            {
                if (_progressController.GetStageType(stage) == StageType.Safe)
                    return stage;
            }
            return _progressController.MaxProgress;
        }
    }
}
