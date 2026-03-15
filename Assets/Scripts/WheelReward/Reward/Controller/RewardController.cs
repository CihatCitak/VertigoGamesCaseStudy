using System;
using Zenject;
using UnityEngine;
using WheelReward.Signals;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.Controller
{
    public class RewardController : IRewardController, IDisposable
    {
        private readonly Dictionary<string, (Sprite sprite, int count, string name)> _rewards = new();
        private readonly IRewardView _rewardView;
        private readonly IRewardEffect _rewardEffect;
        private readonly IYourRewards _yourRewards;
        private readonly SignalBus _signalBus;

        public RewardController(SignalBus signalBus, IRewardView rewardView, IRewardEffect rewardEffect,
            IYourRewards yourRewards)
        {
            _rewardView = rewardView;
            _rewardEffect = rewardEffect;
            _yourRewards = yourRewards;
            _signalBus = signalBus;
            _signalBus.Subscribe<OnSpinRestart>(OnSpinRestart);
        }

        public async UniTask AddReward(string id, string name, Sprite sprite, int count, Vector3 fromPosition)
        {
            try
            {
                if (_rewards.TryGetValue(id, out var existing))
                    _rewards[id] = (existing.sprite, existing.count + count, existing.name);
                else
                    _rewards[id] = (sprite, count, name);

                _rewardView.AddReward(id, sprite);
                await UniTask.DelayFrame(1);

                var toPosition = _rewardView.GetRewardIconWorldPosition(id);
                await _rewardEffect.Play(sprite, fromPosition, toPosition);

                _rewardView.UpdateRewardCount(id, count);
                _signalBus.Fire(new OnSpinEnd());
            }
            catch (Exception e)
            {
                Debug.LogError($"RewardController: AddReward: {e}");
            }
        }

        public void TakeRewards()
        {
            _yourRewards.Show(_rewards);
        }

        private void OnSpinRestart()
        {
            _rewards.Clear();
            _rewardView.ClearAll();
        }

        public void Dispose()
        {
            _signalBus.TryUnsubscribe<OnSpinRestart>(OnSpinRestart);
        }
    }
}