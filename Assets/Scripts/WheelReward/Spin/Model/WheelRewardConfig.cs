using UnityEngine;
using WheelReward.Reward.Model;
using System.Collections.Generic;

namespace WheelReward.Spin.Model
{
    [CreateAssetMenu(fileName = "WheelRewardConfig", menuName = "WheelReward/WheelRewardConfig")]
    public class WheelRewardConfig : ScriptableObject
    {
        [SerializeField] private List<RewardData> rewards = new ();

        public List<RewardData> Rewards => rewards;
    }
}
