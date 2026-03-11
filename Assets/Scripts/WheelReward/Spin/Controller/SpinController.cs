using Zenject;
using UnityEngine;
using WheelReward.Signals;
using Cysharp.Threading.Tasks;
using WheelReward.Spin.Interface;

namespace WheelReward.Spin.Controller
{
    public class SpinController : ISpinStarter
    {
        private readonly SignalBus _signalBus;

        public SpinController(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void StartSpin()
        {
            Debug.Log("Spin Started");
            WaitSpinEnd().Forget();
        }
        
        private async UniTask WaitSpinEnd()
        {
            await UniTask.WaitForSeconds(2);
            SpinEnd();
        }

        private void SpinEnd()
        {
            _signalBus.Fire<OnSpinAvailable>();
        }
    }
}
