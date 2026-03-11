using UnityEngine;
using DG.Tweening;
using Zenject;
using WheelReward.Signals;
using WheelReward.Spin.Model;

namespace WheelReward.Spin.View
{
    public class WheelView : MonoBehaviour
    {
        private const int SlotCount = 8;

        [SerializeField] private WheelTweenData tweenData;
        [SerializeField] private Transform wheelTransform;

        [Inject] private SignalBus _signalBus;

        private Tween _idleTween;
        private Tween _spinTween;

        #region Lifecycle

        private void OnEnable()
        {
            _signalBus.Subscribe<OnSpinStarted>(OnSpinStartedHandler);
        }

        private void OnDisable()
        {
            _signalBus.TryUnsubscribe<OnSpinStarted>(OnSpinStartedHandler);
        }

        private void Start()
        {
            StartIdleTween();
        }

        private void OnDestroy()
        {
            _idleTween?.Kill();
            _spinTween?.Kill();
        }

        #endregion

        #region Idle

        private void StartIdleTween()
        {
            var duration = 360f / tweenData.IdleTweenSpeed;
            _idleTween = wheelTransform
                .DORotate(new Vector3(0f, 0f, -360f), duration, RotateMode.WorldAxisAdd)
                .SetEase(tweenData.IdleTweenEase)
                .SetLoops(-1, LoopType.Incremental);
        }

        #endregion

        #region Spin

        private void OnSpinStartedHandler(OnSpinStarted signal)
        {
            Spin(signal.SlotIndex);
        }

        private void Spin(int slotIndex)
        {
            _idleTween?.Kill();
            _spinTween?.Kill();

            var slotAngle = 360f / SlotCount;
            var currentZ = wheelTransform.eulerAngles.z;
            var targetSlotZ = (360f - slotIndex * slotAngle) % 360f;

            var degreesToTarget = (currentZ - targetSlotZ + 360f) % 360f;
            if (degreesToTarget < Mathf.Epsilon) degreesToTarget = 360f;

            var totalRotation = degreesToTarget + tweenData.SpinTweenExtraRotations * 360f;

            _spinTween = wheelTransform
                .DORotate(new Vector3(0f, 0f, -totalRotation), tweenData.SpinTweenDuration, RotateMode.WorldAxisAdd)
                .SetEase(tweenData.SpinTweenEase)
                .OnComplete(OnSpinComplete);
        }

        private void OnSpinComplete()
        {
            _signalBus.Fire<OnSpinAvailable>();
            StartIdleTween();
        }

        #endregion
    }
}
