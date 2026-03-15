using UnityEngine;
using WheelReward.Reward.Model;
using System.Collections.Generic;

namespace WheelReward.Spin.Model
{
    [CreateAssetMenu(fileName = "WheelRewardConfig", menuName = "WheelReward/WheelRewardConfig")]
    public class WheelRewardConfig : ScriptableObject
    {
        [SerializeField] private List<RewardDataSet> rewardSets = new();

        public List<RewardData> GetRandomRewards()
        {
            if (rewardSets.Count == 0) return new List<RewardData>();
            return rewardSets[Random.Range(0, rewardSets.Count)].Rewards;
        }
    }
}
