using Zenject;
using UnityEngine;
using UnityEngine.UI;
using WheelReward.Signals;
using UnityEngine.EventSystems;
using WheelReward.Spin.Interface;

namespace WheelReward.Spin.View
{
    public class SpinButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Image spinImage;
        
        [Inject] private ISpinStarter _spinStarter;
        [Inject] private readonly SignalBus _signalBus;

        #region Signal Subscriptions

        private void OnEnable()
        {
            _signalBus.Subscribe<OnSpinAvailable>(OpenButton);
        }

        private void OnDisable()
        {
            _signalBus.TryUnsubscribe<OnSpinAvailable>(OpenButton);
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
            spinImage.enabled = false;
        }

        private void OpenButton()
        {
            spinImage.enabled = true;
        }

        #endregion
    }
}