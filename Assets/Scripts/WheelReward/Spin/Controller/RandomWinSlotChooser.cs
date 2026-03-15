using UnityEngine;
using WheelReward.Spin.Interface;

namespace WheelReward.Spin.Controller
{
    public class RandomWinSlotChooser : IWinSlotChooser
    {
        public int ChooseWinSlot(int slotCount) => Random.Range(0, slotCount);
    }
}
