using UnityEngine;

namespace WheelReward.Reward.Interface
{
    public interface IRewardView
    {
        void AddReward(string id, Sprite image);
        void UpdateRewardCount(string id, int count);
        Vector3 GetRewardIconWorldPosition(string id);
    }
}