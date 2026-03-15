using Zenject;
using UnityEngine;
using WheelReward.Signals;
using UnityEngine.EventSystems;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.View
{
    public class RewardExitButton : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly IRewardController _rewardController;

        #region Signal Subscriptions

        private void Awake()
        {
            _signalBus.Subscribe<OnSpinStart>(CloseButton);
            _signalBus.Subscribe<OnSpinEnd>(OpenButton);
            _signalBus.Subscribe<OnSpinRestart>(OpenButton);
            _signalBus.Subscribe<OnSpinKeepRewards>(OpenButton);
        }

        private void OnDestroy()
        {
            _signalBus.TryUnsubscribe<OnSpinStart>(CloseButton);
            _signalBus.TryUnsubscribe<OnSpinEnd>(OpenButton);
            _signalBus.TryUnsubscribe<OnSpinRestart>(OpenButton);
            _signalBus.TryUnsubscribe<OnSpinKeepRewards>(OpenButton);
        }

        #endregion

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

        public void OnPointerDown(PointerEventData eventData)
        {
            _rewardController.TakeRewards();
        }
    }
}