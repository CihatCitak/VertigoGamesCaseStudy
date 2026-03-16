using Zenject;
using DG.Tweening;
using UnityEngine;
using WheelReward.Progress.Model;
using System.Collections.Generic;
using WheelReward.Progress.Interface;

namespace WheelReward.Progress.View
{
    public class ProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private ProgressBarItem itemPrefab;
        [SerializeField] private RectTransform stagesParent;
        [SerializeField] private ProgressBarConfig config;
        [SerializeField] private StageCountDisplay stageCountDisplay;

        [Inject] private IProgressController _progressController;

        private readonly List<ProgressBarItem> _items = new();
        private float _initialX;
        private Tween _slideTween;

        #region Lifecycle

        private void Awake()
        {
            _initialX = stagesParent.anchoredPosition.x;
        }

        private void Start()
        {
            InitializeItemColors();
        }

        private void OnDestroy()
        {
            _slideTween?.Kill();
        }

        #endregion

        public void SetProgress(int progress)
        {
            Slide(progress);
            UpdateItemColors(progress);
            stageCountDisplay.PlayAnimation(progress);
        }

        #region Slide

        private void Slide(int progress)
        {
            _slideTween?.Kill();
            var targetX = _initialX - (progress - 1) * config.BarStepSize;
            _slideTween = DOTween
                .To(() => stagesParent.anchoredPosition.x,
                    x => stagesParent.anchoredPosition = new Vector2(x, stagesParent.anchoredPosition.y),
                    targetX,
                    config.BarTweenDuration)
                .SetEase(Ease.OutQuad);
        }

        #endregion

        #region Item Colors

        private void InitializeItemColors()
        {
            for (var i = 0; i < _progressController.MaxProgress; i++)
            {
                var item = Instantiate(itemPrefab, stagesParent);
                _items.Add(item);

                var stage = i + 1;
                item.SetText(stage);

                var stageType = _progressController.GetStageType(stage);
                if (stageType == StageType.Final)
                    item.SetColor(config.FinalAreaColor);
                else if (stageType == StageType.Safe)
                    item.SetColor(config.SafeAreaColor);
            }
        }

        private void UpdateItemColors(int progress)
        {
            var passedIndex = progress - 2;
            if (passedIndex < 0 || passedIndex >= _items.Count) return;

            _items[passedIndex].DimAlpha(config.BarPassedAlpha);
        }

        #endregion
    }
}
