using UnityEngine;
using System.Collections.Generic;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.View
{
    public class RewardView : MonoBehaviour, IRewardView
    {
        [SerializeField] private RewardItemView itemPrefab;

        private readonly Dictionary<string, RewardItemView> _items = new();

        public void AddReward(string id, Sprite sprite, int count)
        {
            if (_items.TryGetValue(id, out var item))
            {
                item.UpdateCount(count);
            }
            else
            {
                var newItem = Instantiate(itemPrefab, transform);
                newItem.Initialize(id, sprite, count);
                _items[id] = newItem;
            }
        }

        public Vector3 GetRewardIconWorldPosition(string id)
        {
            if (_items.TryGetValue(id, out var item))
                return item.GetIconWorldPosition();

            return transform.position;
        }
    }
}
