using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WheelReward.Reward.Model;

namespace WheelReward.Spin.View
{
    public class WheelRewardView : MonoBehaviour
    {
        [SerializeField] private string id;
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI countText;


        public void Setup(RewardData data)
        {
            id = data.Id;
            rewardImage.sprite = data.Image;
            countText.text = data.Count.ToString();
        }
    }
}
