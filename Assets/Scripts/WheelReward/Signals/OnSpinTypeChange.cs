using WheelReward.Progress.Model;

namespace WheelReward.Signals
{
    public struct OnSpinTypeChange
    {
        public StageType StageType { get; }

        public OnSpinTypeChange(StageType stageType)
        {
            StageType = stageType;
        }
    }
}
