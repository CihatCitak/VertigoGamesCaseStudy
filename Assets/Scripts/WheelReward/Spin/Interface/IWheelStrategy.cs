using WheelReward.Spin.Model;

namespace WheelReward.Spin.Interface
{
    public interface IWheelStrategy
    {
        IWheelView GetCurrentWheelView();
        WheelRewardConfig GetCurrentRewardConfig();
    }
}
