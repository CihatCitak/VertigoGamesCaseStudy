using UnityEngine;
using DG.Tweening;

namespace WheelReward.Spin.Model
{
    [CreateAssetMenu(fileName = "WheelTweenData", menuName = "WheelReward/WheelTweenData")]
    public class WheelTweenData : ScriptableObject
    {
        [SerializeField] private float speed = 90f;
        [SerializeField] private Ease ease = Ease.Linear;

        public float Speed => speed;
        public Ease Ease => ease;
    }
}
