using Zenject;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Spin.Interface;

namespace WheelReward.Spin.Controller
{
    public class SpinController : ISpinStarter
    {
        private const int SlotCount = 8;

        private readonly SignalBus _signalBus;

        public SpinController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void StartSpin()
        {
            var winSlot = Random.Range(0, SlotCount);
            _signalBus.Fire(new OnSpinStarted { SlotIndex = winSlot });
        }
    }
}
