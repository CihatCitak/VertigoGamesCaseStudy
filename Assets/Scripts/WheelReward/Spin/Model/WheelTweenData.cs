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

        [Header("Appear Tween")]
        [SerializeField] private float appearTweenDuration = 0.3f;
        [SerializeField] private Ease appearTweenEase = Ease.OutBack;
        
        public float IdleTweenSpeed => idleTweenSpeed;
        public Ease IdleTweenEase => idleTweenEase;

        public float SpinTweenDuration => spinTweenDuration;
        public Ease SpinTweenEase => spinTweenEase;
        public int SpinTweenExtraRotations => spinTweenExtraRotations;

        public float AppearTweenDuration => appearTweenDuration;
        public Ease AppearTweenEase => appearTweenEase;
    }
}
