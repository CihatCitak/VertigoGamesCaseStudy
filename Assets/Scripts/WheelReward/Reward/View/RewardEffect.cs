using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using WheelReward.Reward.Model;
using WheelReward.Reward.Interface;

namespace WheelReward.Reward.View
{
    public class RewardEffect : MonoBehaviour, IRewardEffect
    {
        [SerializeField] private List<Image> effectImages;
        [SerializeField] private RewardEffectData tweenData;

        private readonly List<Tween> _tweens = new();

        public async UniTask Play(Sprite sprite, Vector3 fromWorldPos, Vector3 toWorldPos)
        {
            KillAllTweens();

            foreach (var img in effectImages)
            {
                img.sprite = sprite;
                img.transform.localPosition = Vector3.zero;
            }

            transform.position = fromWorldPos;
            gameObject.SetActive(true);

            // Phase 1: Scatter
            var scatterTasks = new List<UniTask>();
            foreach (var img in effectImages)
            {
                var angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
                var radius = UnityEngine.Random.Range(0f, tweenData.ScatterRadius);
                var localTarget = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);

                var tween = img.transform
                    .DOLocalMove(localTarget, tweenData.ScatterDuration)
                    .SetEase(tweenData.ScatterEase);
                _tweens.Add(tween);
                scatterTasks.Add(tween.ToUniTask());
            }

            await UniTask.WhenAll(scatterTasks);

            // Phase 2: Hold
            await UniTask.Delay(TimeSpan.FromSeconds(tweenData.HoldDuration));

            // Phase 3: Fly + Gather simultaneously
            var flyAndGatherTasks = new List<UniTask>();

            var flyTween = transform
                .DOMove(toWorldPos, tweenData.FlyDuration)
                .SetEase(tweenData.FlyEase);
            _tweens.Add(flyTween);
            flyAndGatherTasks.Add(flyTween.ToUniTask());

            foreach (var img in effectImages)
            {
                var tween = img.transform
                    .DOLocalMove(Vector3.zero, tweenData.FlyDuration)
                    .SetEase(tweenData.GatherEase);
                _tweens.Add(tween);
                flyAndGatherTasks.Add(tween.ToUniTask());
            }

            await UniTask.WhenAll(flyAndGatherTasks);

            gameObject.SetActive(false);
        }

        private void KillAllTweens()
        {
            foreach (var tween in _tweens)
                tween?.Kill();
            _tweens.Clear();
        }

        private void OnDestroy()
        {
            KillAllTweens();
        }
    }
}
