using System;
using Zenject;
using WheelReward.Signals;
using WheelReward.Lose.Interface;

namespace WheelReward.Lose.Controller
{
    public class LoseController : ILoseController, IDisposable
    {
        private readonly ILoseUI _loseUI;
        private readonly SignalBus _signalBus;

        public LoseController(ILoseUI loseUI, SignalBus signalBus)
        {
            _loseUI = loseUI;
            _signalBus = signalBus;
            _signalBus.Subscribe<OnSpinRestart>(OnSpinRestart);
            _signalBus.Subscribe<OnSpinKeepRewards>(OnKeepRewards);
        }

        public void Lose()
        {
            _loseUI.Show();
        }

        private void OnSpinRestart()
        {
            _loseUI.Hide();
        }

        private void OnKeepRewards()
        {
            _loseUI.Hide();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinRestart>(OnSpinRestart);
            _signalBus.TryUnsubscribe<OnSpinKeepRewards>(OnKeepRewards);
        }
    }
}
