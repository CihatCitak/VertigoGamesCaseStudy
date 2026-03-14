using WheelReward.Progress.Model;

namespace WheelReward.Progress.Interface
{
    public interface IProgressController
    {
        int CurrentStage { get; }
        StageType GetStageType(int stage);
    }
}
