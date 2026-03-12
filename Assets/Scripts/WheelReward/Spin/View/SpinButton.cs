using Zenject;
using UnityEngine;
using WheelReward.Signals;
using UnityEngine.EventSystems;
using WheelReward.Spin.Interface;

namespace WheelReward.Spin.View
{
    public class SpinButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private ISpinStarter _spinStarter;
        [Inject] private readonly SignalBus _signalBus;

        #region Signal Subscriptions

        private void Awake()
        {
            _signalBus.Subscribe<OnSpinEnd>(OpenButton);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<OnSpinEnd>(OpenButton);
        }

        #endregion


        public void OnPointerDown(PointerEventData eventData)
        {
            _spinStarter.StartSpin();
            CloseButton();
        }

        #region Button Toggle

        private void CloseButton()
        {
            gameObject.SetActive(false);
        }

        private void OpenButton()
        {
            gameObject.SetActive(true);
        }

        #endregion
    }
}