using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace WheelReward.Reward.View
{
    public class RewardItemView : MonoBehaviour
    {
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI countText;

        private int _displayedCount;
        private Tween _countTween;

        public string Id { get; private set; }

        public void Initialize(string id, Sprite sprite, int count)
        {
            Id = id;
            rewardImage.sprite = sprite;
            _displayedCount = count;
            countText.text = count.ToString();
        }

        public void UpdateCount(int addedCount)
        {
            _countTween?.Kill();
            var targetCount = _displayedCount + addedCount;
            _countTween = DOTween
                .To(() => _displayedCount,
                    x =>
                    {
                        _displayedCount = x;
                        countText.text = x.ToString();
                    },
                    targetCount,
                    0.5f)
                .SetEase(Ease.OutQuad);
        }

        private void OnDestroy()
        {
            _countTween?.Kill();
        }
    }
}
