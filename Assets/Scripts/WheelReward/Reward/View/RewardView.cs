using UnityEngine;
using System.Collections.Generic;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.View
{
    public class RewardView : MonoBehaviour, IRewardView
    {
        [SerializeField] private RewardItemView itemPrefab;

        private readonly Dictionary<string, RewardItemView> _items = new();

        public void AddReward(string id, Sprite sprite)
        {
            if (_items.ContainsKey(id))
                return;

            var newItem = Instantiate(itemPrefab, transform);
            newItem.Initialize(id, sprite, 0);
            _items[id] = newItem;
        }

        public void UpdateRewardCount(string id, int count)
        {
            if (_items.TryGetValue(id, out var item))
                item.UpdateCount(count);
        }

        public Vector3 GetRewardIconWorldPosition(string id)
        {
            if (_items.TryGetValue(id, out var item))
                return item.GetIconWorldPosition();

            return transform.position;
        }

        public void ClearAll()
        {
            foreach (var item in _items.Values)
                Destroy(item.gameObject);
            _items.Clear();
        }
    }
}
