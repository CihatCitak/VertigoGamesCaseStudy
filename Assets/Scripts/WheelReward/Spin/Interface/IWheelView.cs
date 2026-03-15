using UnityEngine;
using Cysharp.Threading.Tasks;
using WheelReward.Reward.Model;
using System.Collections.Generic;

namespace WheelReward.Spin.Interface
{
    public interface IWheelView
    {
        int SlotCount { get; }
        void SetupRewards(List<RewardData> rewards, int progress, int maxProgress);
        UniTask<RewardData> Spin(int slotIndex);
        Vector3 GetSlotWorldPosition(int slotIndex);
    }
}
