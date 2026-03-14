using System;
using Zenject;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Spin.Interface;
using WheelReward.Reward.Interface;
using Random = UnityEngine.Random;

namespace WheelReward.Spin.Controller
{
    public class SpinController : ISpinStarter
    {
        private const int SlotCount = 8;
        private readonly SignalBus _signalBus;
        private readonly IRewardController _rewardController;
        private readonly IWheelStrategy _wheelStrategy;

        public SpinController(SignalBus signalBus, IRewardController rewardController, IWheelStrategy wheelStrategy)
        {
            _signalBus = signalBus;
            _rewardController = rewardController;
            _wheelStrategy = wheelStrategy;
        }

        public async void StartSpin()
        {
            try
            {
                var wheelView = _wheelStrategy.GetCurrentWheelView();
                var rewardConfig = _wheelStrategy.GetCurrentRewardConfig();

                var winSlot = Random.Range(0, SlotCount);
                var rewardData = rewardConfig.Rewards[winSlot];

                _signalBus.Fire(new OnSpinStart());

                await wheelView.Spin(winSlot);
                _rewardController.AddReward(rewardData.Id, rewardData.Image, rewardData.Count);

                _signalBus.Fire(new OnSpinEnd());
            }
            catch (Exception e)
            {
                Debug.LogError($"SpinController: StartSpin: {e}");
            }
        }
    }
}
