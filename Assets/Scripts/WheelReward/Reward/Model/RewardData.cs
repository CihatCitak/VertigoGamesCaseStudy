using System;
using UnityEngine;

namespace WheelReward.Reward.Model
{
    [Serializable]
    public class RewardData
    {
        [SerializeField] private string id;
        [SerializeField] private Sprite image;
        [SerializeField] private int count;

        public string Id => id;
        public Sprite Image => image;
        public int Count => count;
    }
}
