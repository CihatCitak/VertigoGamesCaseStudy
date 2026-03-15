using UnityEngine;

namespace WheelReward.Reward.Interface
{
    public interface IRewardView
    {
        void AddReward(string id, Sprite image, int count);
        Vector3 GetRewardIconWorldPosition(string id);
    }
}