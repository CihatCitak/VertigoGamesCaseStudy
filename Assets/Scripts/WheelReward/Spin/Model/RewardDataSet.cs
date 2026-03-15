using System;
using System.Collections.Generic;
using UnityEngine;
using WheelReward.Reward.Model;

namespace WheelReward.Spin.Model
{
    [Serializable]
    public class RewardDataSet
    {
        [SerializeField] private List<RewardData> rewards = new();
        public List<RewardData> Rewards => rewards;
    }
}
