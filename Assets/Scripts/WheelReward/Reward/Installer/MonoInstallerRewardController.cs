using Zenject;
using UnityEngine;
using WheelReward.Reward.View;
using WheelReward.Reward.Controller;

namespace WheelReward.Reward.Installer
{
    public class MonoInstallerRewardController : MonoInstaller
    {
        [SerializeField] private RewardView rewardView;
        [SerializeField] private RewardEffect rewardEffect;
        [SerializeField] private TakeRewards takeRewards;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RewardController>()
                .AsSingle()
                .WithArguments(rewardView, rewardEffect, takeRewards)
                .NonLazy();
        }
    }
}