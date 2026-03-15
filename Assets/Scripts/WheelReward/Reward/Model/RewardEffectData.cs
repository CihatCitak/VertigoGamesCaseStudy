using UnityEngine;
using DG.Tweening;

namespace WheelReward.Reward.Model
{
    [CreateAssetMenu(fileName = "RewardEffectData", menuName = "WheelReward/RewardEffectData")]
    public class RewardEffectData : ScriptableObject
    {
        [Header("Scatter")]
        [SerializeField] private float scatterRadius = 100f;
        [SerializeField] private float scatterDuration = 0.3f;
        [SerializeField] private Ease scatterEase = Ease.OutQuad;

        [Header("Hold")]
        [SerializeField] private float holdDuration = 0.3f;

        [Header("Fly to Target")]
        [SerializeField] private float flyDuration = 0.5f;
        [SerializeField] private Ease flyEase = Ease.InBack;
        [SerializeField] private Ease gatherEase = Ease.InBack;

        public float ScatterRadius => scatterRadius;
        public float ScatterDuration => scatterDuration;
        public Ease ScatterEase => scatterEase;

        public float HoldDuration => holdDuration;

        public float FlyDuration => flyDuration;
        public Ease FlyEase => flyEase;
        public Ease GatherEase => gatherEase;
    }
}
