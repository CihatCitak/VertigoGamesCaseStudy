using UnityEngine;
using Cysharp.Threading.Tasks;

namespace WheelReward.Reward.Interface
{
    public interface IRewardController
    {
        UniTask AddReward(string id, Sprite image, int count, Vector3 fromPosition);
        void TakeRewards();
    }
}
