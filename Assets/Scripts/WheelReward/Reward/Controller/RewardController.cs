using UnityEngine;
using System.Collections.Generic;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.Controller
{
    public class RewardController : IRewardController
    {
        private readonly Dictionary<string, int> _rewards = new();
        private readonly IRewardView _rewardView;

        public RewardController(IRewardView rewardView)
        {
            _rewardView = rewardView;
        }

        public void AddReward(string id, Sprite sprite, int count)
        {
            if (!_rewards.TryAdd(id, count))
                _rewards[id] += count;

            _rewardView.AddReward(id, sprite, count);
        }

        public void TakeRewards()
        {
            Debug.Log("RewardController: Take rewards");
        }
    }
}