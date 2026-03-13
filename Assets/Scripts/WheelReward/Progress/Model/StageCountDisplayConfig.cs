using DG.Tweening;
using UnityEngine;

namespace WheelReward.Progress.Model
{
    [CreateAssetMenu(fileName = "StageCountDisplayConfig", menuName = "WheelReward/StageCountDisplayConfig")]
    public class StageCountDisplayConfig : ScriptableObject
    {
        [Header("Animation")]
        [SerializeField] private float tweenDuration = 0.4f;
        [SerializeField] private float slideDistance = 90f;
        [SerializeField] private float slideDelay = 0.1f;
        [SerializeField] private Ease outEase = Ease.InOutQuad;
        [SerializeField] private Ease inEase = Ease.InOutQuad;

        [Header("Sprites")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite safeStageSprite;

        public float TweenDuration => tweenDuration;
        public float SlideDistance => slideDistance;
        public float SlideDelay => slideDelay;
        public Ease OutEase => outEase;
        public Ease InEase => inEase;
        public Sprite DefaultSprite => defaultSprite;
        public Sprite SafeStageSprite => safeStageSprite;
    }
}
