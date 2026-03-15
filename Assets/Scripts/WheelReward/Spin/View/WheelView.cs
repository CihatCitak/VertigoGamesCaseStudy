using Zenject;
using DG.Tweening;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Spin.Model;
using Cysharp.Threading.Tasks;
using WheelReward.Spin.Interface;
using WheelReward.Reward.Model;
using System.Collections.Generic;

namespace WheelReward.Spin.View
{
    public class WheelView : MonoBehaviour, IWheelView
    {
        [SerializeField] private WheelTweenData tweenData;
        [SerializeField] private Transform wheelTransform;
        [SerializeField] private List<WheelRewardView> wheelRewardViews;

        [Inject] private SignalBus _signalBus;

        private List<RewardData> _currentRewards;
        private Tween _idleTween;
        private Tween _spinTween;
        private Tween _appearTween;

        #region Lifecycle

        private void Start()
        {
            _signalBus.Subscribe<OnSpinEnd>(StartIdleTween);
            StartIdleTween();
        }

        private void OnDestroy()
        {
            _idleTween?.Kill();
            _spinTween?.Kill();
            _appearTween?.Kill();
            _signalBus.TryUnsubscribe<OnSpinEnd>(StartIdleTween);
        }

        private void OnDisable()
        {
            _idleTween?.Kill();
            _spinTween?.Kill();
            _appearTween?.Kill();
        }

        #endregion

        #region Setup

        public int SlotCount => wheelRewardViews.Count;

        public void SetupRewards(List<RewardData> rewards, int progress, int maxProgress)
        {
            _currentRewards = rewards;
            for (var i = 0; i < rewards.Count; i++)
            {
                var currentCount = rewards[i].GetBlendedCount(progress, maxProgress);
                rewards[i].CurrenCount  = currentCount;
                wheelRewardViews[i].Setup(rewards[i], rewards[i].GetBlendedCount(progress, maxProgress));
                
            }
        }

        #endregion

        #region Idle Tween

        private void StartIdleTween()
        {
            _idleTween?.Kill();
            var duration = 360f / tweenData.IdleTweenSpeed;
            _idleTween = wheelTransform
                .DORotate(new Vector3(0f, 0f, -360f), duration, RotateMode.WorldAxisAdd)
                .SetEase(tweenData.IdleTweenEase)
                .SetLoops(-1, LoopType.Incremental);
        }

        #endregion

        #region Appear Tween

        public void PlayShowTween()
        {
            _appearTween?.Kill();
            _spinTween?.Kill();

            transform.localScale = Vector3.zero;
            _appearTween = transform
                .DOScale(Vector3.one, tweenData.AppearTweenDuration)
                .SetEase(tweenData.AppearTweenEase);
        }

        #endregion

        #region Spin Tween

        public async UniTask<RewardData> Spin(int slotIndex)
        {
            _idleTween?.Kill();
            _spinTween?.Kill();
            _appearTween?.Kill();

            var slotAngle = 360f / SlotCount;
            var currentZ = wheelTransform.eulerAngles.z;
            var targetSlotZ = (slotIndex * slotAngle) % 360f;

            var degreesToTarget = (currentZ - targetSlotZ + 360f) % 360f;
            if (degreesToTarget < Mathf.Epsilon) degreesToTarget = 360f;

            var totalRotation = degreesToTarget + tweenData.SpinTweenExtraRotations * 360f;

            _spinTween = wheelTransform
                .DORotate(new Vector3(0f, 0f, -totalRotation), tweenData.SpinTweenDuration, RotateMode.WorldAxisAdd)
                .SetEase(tweenData.SpinTweenEase);

            await _spinTween.ToUniTask();
            return _currentRewards[slotIndex];
        }

        public Vector3 GetSlotWorldPosition(int slotIndex)
        {
            return wheelRewardViews[slotIndex].transform.position;
        }

        #endregion
    }
}
