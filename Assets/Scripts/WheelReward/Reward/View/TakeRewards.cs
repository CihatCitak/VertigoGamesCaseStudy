using Zenject;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using WheelReward.Signals;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.View
{
    public class TakeRewards : MonoBehaviour, IYourRewards
    {
        [SerializeField] private TakeRewardsItemView itemPrefab;
        [SerializeField] private Transform itemContainer;
        [SerializeField] private float showDuration = 0.3f;
        [SerializeField] private Ease showEase = Ease.OutBack;

        [Inject] private readonly SignalBus _signalBus;

        private readonly List<TakeRewardsItemView> _items = new();
        private Tween _showTween;

        #region Signal Subscriptions

        private void Awake()
        {
            _signalBus.Subscribe<OnSpinRestart>(OnSpinRestart);
        }

        private void OnDestroy()
        {
            _showTween?.Kill();
            _signalBus.TryUnsubscribe<OnSpinRestart>(OnSpinRestart);
        }

        #endregion

        public void Show(IReadOnlyDictionary<string, (Sprite sprite, int count, string name)> rewards)
        {
            ClearItems();
            gameObject.SetActive(true);
            foreach (var entry in rewards.Values)
            {
                var item = Instantiate(itemPrefab, itemContainer);
                item.Initialize(entry.sprite, entry.count, entry.name);
                _items.Add(item);
            }

            transform.localScale = Vector3.zero;
            _showTween?.Kill();
            _showTween = transform.DOScale(Vector3.one, showDuration).SetEase(showEase);
        }

        private void OnSpinRestart()
        {
            ClearItems();
            gameObject.SetActive(false);
        }

        private void ClearItems()
        {
            foreach (var item in _items)
                Destroy(item.gameObject);
            _items.Clear();
        }
    }
}
