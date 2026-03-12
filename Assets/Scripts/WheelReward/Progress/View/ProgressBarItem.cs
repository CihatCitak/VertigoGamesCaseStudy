using TMPro;
using UnityEngine;

namespace WheelReward.Progress.View
{
    public class ProgressBarItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        public void SetColor(Color color)
        {
            if (label != null)
                label.color = color;
        }

        public void DimAlpha(float alpha)
        {
            var color = label.color;
            color.a = alpha;
            label.color = color;
        }
    }
}