using UnityEngine;

namespace WheelReward.Progress.Model
{
    [CreateAssetMenu(fileName = "ProgressBarConfig", menuName = "WheelReward/ProgressBarConfig")]
    public class ProgressBarConfig : ScriptableObject
    {
        [Header("Stage Settings")]
        [SerializeField] private int finalStage = 30;
        [SerializeField] private int milestoneInterval = 5;

        [Header("Progress Bar")]
        [SerializeField] private float barStepSize = 85f;
        [SerializeField] private float barTweenDuration = 0.4f;
        [SerializeField] private float barPassedAlpha = 0.75f;

        [Header("Progress Text Colors")]
        [SerializeField] private Color safeAreaColor = Color.green;
        [SerializeField] private Color finalAreaColor = Color.yellow;

        public int FinalStage => finalStage;
        public int MilestoneInterval => milestoneInterval;
        public float BarStepSize => barStepSize;
        public float BarTweenDuration => barTweenDuration;
        public float BarPassedAlpha => barPassedAlpha;
        public Color SafeAreaColor => safeAreaColor;
        public Color FinalAreaColor => finalAreaColor;
    }
}
