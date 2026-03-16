using TMPro;
using UnityEngine;
using System.Text;
using UnityEngine.UI;
using WheelReward.Reward.Model;

namespace WheelReward.Spin.View
{
    public class WheelRewardView : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI countText;

        private readonly StringBuilder _builder = new();

        private RectTransform _rewardRect;
        private Vector2 _originalAnchorMin;
        private Vector2 _originalAnchorMax;
        private Vector2 _originalAnchoredPosition;
        private Vector2 _originalSizeDelta;

        private void Awake()
        {
            _rewardRect = rewardImage.rectTransform;
            _originalAnchorMin = _rewardRect.anchorMin;
            _originalAnchorMax = _rewardRect.anchorMax;
            _originalAnchoredPosition = _rewardRect.anchoredPosition;
            _originalSizeDelta = _rewardRect.sizeDelta;
        }

        public void Setup(RewardData data, int blendedCount)
        {
            id = data.Id;
            rewardImage.sprite = data.Icon;

            if (data.IsBomb)
            {
                StretchRewardImage();
                countText.gameObject.SetActive(false);
                return;
            }

            RestoreRewardImage();
            countText.gameObject.SetActive(true);
            _builder.Clear();
            _builder.Append("x");
            _builder.Append(blendedCount.ToString());
            countText.text = _builder.ToString();
        }

        private void StretchRewardImage()
        {
            _rewardRect.anchorMin = Vector2.zero;
            _rewardRect.anchorMax = Vector2.one;
            _rewardRect.offsetMin = Vector2.zero;
            _rewardRect.offsetMax = Vector2.zero;
        }

        private void RestoreRewardImage()
        {
            _rewardRect.anchorMin = _originalAnchorMin;
            _rewardRect.anchorMax = _originalAnchorMax;
            _rewardRect.anchoredPosition = _originalAnchoredPosition;
            _rewardRect.sizeDelta = _originalSizeDelta;
        }
    }
}
