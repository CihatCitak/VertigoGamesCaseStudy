using DG.Tweening;
using UnityEngine;
using WheelReward.Progress.Model;
using System.Collections.Generic;
using WheelReward.Progress.Interface;

namespace WheelReward.Progress.View
{
    public class ProgressBar : MonoBehaviour, IProgressBar
    {
        [SerializeField] private RectTransform stagesParent;
        [SerializeField] private List<ProgressBarItem> items;
        [SerializeField] private ProgressBarConfig config;
        [SerializeField] private StageCountDisplay stageCountDisplay;

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
            for (var i = 0; i < items.Count; i++)
            {
                var stage = i + 1;
                if (stage == config.FinalStage)
                    items[i].SetColor(config.FinalColor);
                else if (stage == 1 || stage % config.MilestoneInterval == 0)
                    items[i].SetColor(config.MilestoneColor);
            }
        }

        private void UpdateItemColors(int progress)
        {
            var passedIndex = progress - 2;
            if (passedIndex < 0 || passedIndex >= items.Count) return;
            
            items[passedIndex].DimAlpha(config.BarPassedAlpha);
        }

        #endregion
    }
}
