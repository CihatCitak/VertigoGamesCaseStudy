using Zenject;
using UnityEngine;
using WheelReward.Signals;
using UnityEngine.EventSystems;

namespace WheelReward.Reward.View
{
    public class ClaimButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private readonly SignalBus _signalBus;

        public void OnPointerDown(PointerEventData eventData)
        {
            _signalBus.Fire(new OnSpinRestart());
        }
    }
}
