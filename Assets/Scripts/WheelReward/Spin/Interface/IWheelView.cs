using UnityEngine;
using Cysharp.Threading.Tasks;

namespace WheelReward.Spin.Interface
{
    public interface IWheelView
    {
        UniTask Spin(int slotIndex);
        Vector3 GetSlotWorldPosition(int slotIndex);
    }
}