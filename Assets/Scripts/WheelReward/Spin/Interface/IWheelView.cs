using Cysharp.Threading.Tasks;

namespace WheelReward.Spin.Interface
{
    public interface IWheelView
    {
        public UniTask Spin(int slotIndex);
    }
}