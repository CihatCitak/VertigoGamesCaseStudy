using System;
using Zenject;
using WheelReward.Signals;
using WheelReward.Progress.Interface;

namespace WheelReward.Progress.Controller
{
    public class ProgressController : IProgressController, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly IProgressBar _progressBar;

        private int _progress = 1;

        public ProgressController(SignalBus signalBus, IProgressBar progressBar)
        {
            _signalBus = signalBus;
            _progressBar = progressBar;
            _signalBus.Subscribe<OnSpinEnd>(OnSpinEnd);
        }

        private void OnSpinEnd()
        {
            _progress++;
            _progressBar.SetProgress(_progress);
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinEnd>(OnSpinEnd);
        }
    }
}