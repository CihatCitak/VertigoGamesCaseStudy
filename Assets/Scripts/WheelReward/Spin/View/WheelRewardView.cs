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

        public void Setup(RewardData data, int blendedCount)
        {
            id = data.Id;
            rewardImage.sprite = data.Icon;

            if (data.IsBomb)
            {
                countText.gameObject.SetActive(false);
                return;
            }

            _builder.Clear();
            _builder.Append("x");
            _builder.Append(blendedCount.ToString());
            countText.text = _builder.ToString();
        }
    }
}
