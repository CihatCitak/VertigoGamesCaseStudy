using System;
using Zenject;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using WheelReward.Signals;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.Controller
{
    public class RewardController : IRewardController
    {
        private readonly Dictionary<string, int> _rewards = new();
        private readonly IRewardView _rewardView;
        private readonly IRewardEffect _rewardEffect;
        private readonly SignalBus _signalBus;

        public RewardController(IRewardView rewardView, IRewardEffect rewardEffect, SignalBus signalBus)
        {
            _rewardView = rewardView;
            _rewardEffect = rewardEffect;
            _signalBus = signalBus;
        }

        public async UniTask AddReward(string id, Sprite sprite, int count, Vector3 fromPosition)
        {
            try
            {
                if (!_rewards.TryAdd(id, count))
                    _rewards[id] += count;

                _rewardView.AddReward(id, sprite);
                await UniTask.DelayFrame(1);
                var toPosition = _rewardView.GetRewardIconWorldPosition(id);
                await _rewardEffect.Play(sprite, fromPosition, toPosition);
                _rewardView.UpdateRewardCount(id, count);
                _signalBus.Fire(new OnSpinEnd());
            }
            catch (Exception e)
            {
                Debug.LogError($"RewardController: AddReward: {e}");
            }
        }

        public void TakeRewards()
        {
            Debug.Log("RewardController: Take rewards");
        }
    }
}
