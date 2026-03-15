using UnityEngine;
using Cysharp.Threading.Tasks;

namespace WheelReward.Reward.Interface
{
    public interface IRewardEffect
    {
        UniTask Play(Sprite sprite, Vector3 fromWorldPos, Vector3 toWorldPos);
    }
}
