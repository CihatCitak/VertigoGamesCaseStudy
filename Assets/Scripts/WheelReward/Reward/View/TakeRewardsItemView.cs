using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelReward.Reward.View
{
    public class TakeRewardsItemView : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI countText;

        public void Initialize(Sprite sprite, int count, string name)
        {
            icon.sprite = sprite;
            nameText.text = name;
            countText.text = count.ToString();
        }
    }
}
