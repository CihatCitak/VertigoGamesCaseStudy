using System;
using Zenject;
using DG.Tweening;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Spin.Model;
using Cysharp.Threading.Tasks;
using WheelReward.Spin.Interface;
using System.Collections.Generic;

namespace WheelReward.Spin.View
{
    public class WheelView : MonoBehaviour, IWheelView
    {
        //[Header("Tween Attributes")] 
        [SerializeField] private WheelTweenData tweenData;
        [SerializeField] private Transform wheelTransform;

        //[Header("Reward Settings")] 
        [SerializeField] private WheelRewardConfig wheelRewardConfig;
        [SerializeField] private List<WheelRewardView> wheelRewardViews;

        [Inject] private SignalBus _signalBus;

        private Tween _idleTween;
        private Tween _spinTween;
        private Tween _appearTween;
        private const int SlotCount = 8;

        #region Lifecycle

        private void Awake()
        {
            _signalBus.Subscribe<OnSpinEnd>(StartIdleTween);
        }

        private void Start()
        {
            SetupRewards();
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

        private void OnValidate()
        {
            if (wheelRewardConfig == null || wheelRewardViews == null) return;

            var rewardCount = wheelRewardConfig.Rewards.Count;
            var viewCount = wheelRewardViews.Count;

            if (rewardCount == viewCount) return;
            
            Debug.LogWarning(
                $"[WheelView] Reward count ({rewardCount}) and view count ({viewCount}) do not match!");

            Debug.LogWarning(
                viewCount > rewardCount
                    ? $"[WheelView] Trimmed {viewCount - rewardCount} excess view(s) from the list."
                    : $"[WheelView] Missing {rewardCount - viewCount} view(s). Please assign them in the Inspector.");
        }

        private void SetupRewards()
        {
            if (wheelRewardConfig == null || wheelRewardViews == null) return;

            var rewardCount = wheelRewardConfig.Rewards.Count;
            var viewCount = wheelRewardViews.Count;

            if (rewardCount != viewCount)
            {
                Debug.LogError(
                    $"[WheelView] Cannot setup rewards: count mismatch (rewards: {rewardCount}, views: {viewCount}).");
                return;
            }

            for (var i = 0; i < rewardCount; i++)
            {
                wheelRewardViews[i].Setup(wheelRewardConfig.Rewards[i]);
            }
        }

        #endregion

        #region Idle Tween

        private void StartIdleTween()
        {
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

        public async UniTask Spin(int slotIndex)
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
        }

        public Vector3 GetSlotWorldPosition(int slotIndex)
        {
            return wheelRewardViews[slotIndex].transform.position;
        }

        #endregion
    }
}