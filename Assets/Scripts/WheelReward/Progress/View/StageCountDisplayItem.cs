using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WheelReward.Progress.View
{
    public class StageCountDisplayItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private Image background;

        public RectTransform RectTransform { get; private set; }
        public Vector2 StartPosition { get; private set; }

        private void Awake()
        {
            RectTransform = GetComponent<RectTransform>();
            StartPosition = RectTransform.anchoredPosition;
        }

        public void Setup(int stage, Color color, Sprite sprite)
        {
            label.text = stage.ToString();
            label.color = color;
            background.sprite = sprite;
        }
    }
}
