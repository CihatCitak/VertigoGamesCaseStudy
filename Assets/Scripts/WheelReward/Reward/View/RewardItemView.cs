using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

        public void UpdateCount(int newCount)
        {
            _countTween?.Kill();
            _countTween = DOTween
                .To(() => _displayedCount,
                    x =>
                    {
                        _displayedCount = x;
                        countText.text = x.ToString();
                    },
                    newCount,
                    0.5f)
                .SetEase(Ease.OutQuad);
        }

        private void OnDestroy()
        {
            _countTween?.Kill();
        }
    }
}
