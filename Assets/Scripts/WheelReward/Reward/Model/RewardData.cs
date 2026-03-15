using System;
using UnityEngine;

namespace WheelReward.Reward.Model
{
    [Serializable]
    public class RewardData
    {
        [SerializeField] private string id;
        [SerializeField] private string name;
        [SerializeField] private Sprite icon;
        [SerializeField] private int minCount;
        [SerializeField] private int maxCount;
        [SerializeField] private bool isBomb;

        [HideInInspector] public int CurrenCount;

        public string Id => id;
        public string Name => name;
        public Sprite Icon => icon;
        public int MinCount => minCount;
        public int MaxCount => maxCount;
        public bool IsBomb => isBomb;

        public int GetBlendedCount(int progress, int maxProgress)
        {
            var t = maxProgress <= 1 ? 1f : (float)(progress - 1) / (maxProgress - 1);
            return Mathf.RoundToInt(Mathf.Lerp(minCount, maxCount, t));
        }
    }
}