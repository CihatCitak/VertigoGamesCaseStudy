using Zenject;
using UnityEngine;
using UnityEngine.EventSystems;
using WheelReward.Signals;

namespace WheelReward.Lose.View
{
    public class LoseButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private SignalBus _signalBus;

        public void OnPointerDown(PointerEventData eventData)
        {
            _signalBus.Fire(new OnSpinRestart());
        }
    }
}
