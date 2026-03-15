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

        public void Setup(RewardData data)
        {
            id = data.Id;
            rewardImage.sprite = data.Image;

            if (data.IsBomb)
            {
                countText.gameObject.SetActive(false);
                return;
            }

            _builder.Append("x");
            _builder.Append(data.Count.ToString());
            countText.text = _builder.ToString();
        }
    }
}