using UnityEngine;
using DG.Tweening;
using WheelReward.Spin.Model;

namespace WheelReward.Spin.View
{
    public class WheelView : MonoBehaviour
    {
        [SerializeField] private WheelTweenData tweenData;
        [SerializeField] private Transform wheelTransform;

        private Tween _idleTween;

        private void Start()
        {
            StartIdleTween();
        }

        private void OnDestroy()
        {
            _idleTween?.Kill();
        }

        private void StartIdleTween()
        {
            var duration = 360f / tweenData.Speed;
            _idleTween = wheelTransform
                .DORotate(new Vector3(0f, 0f, -360f), duration, RotateMode.FastBeyond360)
                .SetEase(tweenData.Ease)
                .SetLoops(-1, LoopType.Restart);
        }
    }
}
