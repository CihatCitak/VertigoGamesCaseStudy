using System;
using UnityEngine;

namespace WheelReward.Reward.Model
{
    [Serializable]
    public class RewardData
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private Sprite image;
        [SerializeField] private int count;
        [SerializeField] private bool isBomb;

        public string Id => id;
        public string Name => name;
        public Sprite Image => image;
        public int Count => count;
        public bool IsBomb => isBomb;
    }
}
