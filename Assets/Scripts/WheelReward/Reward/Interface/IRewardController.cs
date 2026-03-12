using UnityEngine;

namespace WheelReward.Reward.Interface
{
    public interface IRewardController
    {
        void AddReward(string id, Sprite image, int count);
        void TakeRewards();
    }
}
