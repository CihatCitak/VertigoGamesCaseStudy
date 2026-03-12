using UnityEngine;

namespace WheelReward.Progress.Model
{
    [CreateAssetMenu(fileName = "ProgressBarConfig", menuName = "WheelReward/ProgressBarConfig")]
    public class ProgressBarConfig : ScriptableObject
    {
        [Header("Animation")]
        [SerializeField] private float stepSize = 85f;
        [SerializeField] private float tweenDuration = 0.4f;

        [Header("Milestone Settings")]
        [SerializeField] private int milestoneInterval = 5;
        [SerializeField] private int finalStage = 30;

        [Header("Colors")]
        [SerializeField] private float passedAlpha = 0.75f;
        [SerializeField] private Color milestoneColor = Color.green;
        [SerializeField] private Color finalColor = Color.yellow;

        public float StepSize => stepSize;
        public float TweenDuration => tweenDuration;
        public int MilestoneInterval => milestoneInterval;
        public int FinalStage => finalStage;
        public float PassedAlpha => passedAlpha;
        public Color MilestoneColor => milestoneColor;
        public Color FinalColor => finalColor;
    }
}
