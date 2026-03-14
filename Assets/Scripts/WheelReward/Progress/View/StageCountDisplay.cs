using DG.Tweening;
using Zenject;
using UnityEngine;
using WheelReward.Progress.Model;
using WheelReward.Progress.Interface;

namespace WheelReward.Progress.View
{
    public class StageCountDisplay : MonoBehaviour
    {
        [SerializeField] private StageCountDisplayItem itemA;
        [SerializeField] private StageCountDisplayItem itemB;
        [SerializeField] private ProgressBarConfig config;
        [SerializeField] private StageCountDisplayConfig counterConfig;

        [Inject] private IProgressController _progressController;

        private StageCountDisplayItem _currentItem;
        private StageCountDisplayItem _incomingItem;
        private Vector2 _centerPos;
        private Vector2 _rightPos;
        private int _currentStage = 1;
        private Tween _outTween;
        private Tween _inTween;

        #region Lifecycle

        private void Awake()
        {
            _currentItem = itemA;
            _incomingItem = itemB;
        }

        private void Start()
        {
            _centerPos = _currentItem.StartPosition;
            _rightPos = _incomingItem.StartPosition;
            _currentItem.Setup(1, GetColorForStage(1), GetSpriteForStage(1));
        }

        private void OnDestroy()
        {
            _outTween?.Kill();
            _inTween?.Kill();
        }

        #endregion

        public void PlayAnimation(int newStage)
        {
            if (newStage == _currentStage) return;

            _incomingItem.Setup(newStage, GetColorForStage(newStage), GetSpriteForStage(newStage));

            _outTween?.Kill();
            _inTween?.Kill();

            _outTween = DOTween
                .To(() => _currentItem.RectTransform.anchoredPosition.x,
                    x => _currentItem.RectTransform.anchoredPosition = new Vector2(x, _centerPos.y),
                    _centerPos.x - counterConfig.SlideDistance,
                    counterConfig.TweenDuration)
                .SetEase(counterConfig.OutEase);

            _inTween = DOTween
                .To(() => _incomingItem.RectTransform.anchoredPosition.x,
                    x => _incomingItem.RectTransform.anchoredPosition = new Vector2(x, _rightPos.y),
                    _centerPos.x,
                    counterConfig.TweenDuration)
                .SetEase(counterConfig.InEase)
                .SetDelay(counterConfig.SlideDelay)
                .OnComplete(() =>
                {
                    _currentItem.RectTransform.anchoredPosition = _rightPos;
                    (_currentItem, _incomingItem) = (_incomingItem, _currentItem);
                    _currentStage = newStage;
                });
        }

        #region Helpers

        private Color GetColorForStage(int stage)
        {
            var type = _progressController.GetStageType(stage);
            if (type == StageType.Final) return config.FinalColor;
            if (type == StageType.Safe) return config.MilestoneColor;
            return Color.white;
        }

        private Sprite GetSpriteForStage(int stage)
        {
            var type = _progressController.GetStageType(stage);
            return type == StageType.Normal ? counterConfig.DefaultSprite : counterConfig.SafeStageSprite;
        }

        #endregion
    }
}
