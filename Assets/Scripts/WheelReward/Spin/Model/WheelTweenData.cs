using UnityEngine;
using DG.Tweening;

namespace WheelReward.Spin.Model
{
    [CreateAssetMenu(fileName = "WheelTweenData", menuName = "WheelReward/WheelTweenData")]
    public class WheelTweenData : ScriptableObject
    {
        [Header("Idle Tween")]
        [SerializeField] private float idleTweenSpeed = 90f;
        [SerializeField] private Ease idleTweenEase = Ease.Linear;

        [Header("Spin Tween")]
        [SerializeField] private float spinTweenDuration = 3f;
        [SerializeField] private Ease spinTweenEase = Ease.OutQuart;
        [SerializeField] private int spinTweenExtraRotations = 5;

        public float IdleTweenSpeed => idleTweenSpeed;
        public Ease IdleTweenEase => idleTweenEase;

        public float SpinTweenDuration => spinTweenDuration;
        public Ease SpinTweenEase => spinTweenEase;
        public int SpinTweenExtraRotations => spinTweenExtraRotations;
    }
}
