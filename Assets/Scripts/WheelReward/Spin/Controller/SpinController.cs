using System;
using Zenject;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Spin.Model;
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
        private readonly IWheelView _wheelView;
        private readonly WheelRewardConfig _rewardConfig;

        public SpinController(SignalBus signalBus, IRewardController rewardController, IWheelView wheelView,
            WheelRewardConfig rewardConfig)
        {
            _signalBus = signalBus;
            _rewardController = rewardController;
            _wheelView = wheelView;
            _rewardConfig = rewardConfig;
        }

        public async void StartSpin()
        {
            try
            {
                var winSlot = Random.Range(0, SlotCount);
                var rewardData = _rewardConfig.Rewards[winSlot];

                _signalBus.Fire(new OnSpinStart());

                await _wheelView.Spin(winSlot);
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