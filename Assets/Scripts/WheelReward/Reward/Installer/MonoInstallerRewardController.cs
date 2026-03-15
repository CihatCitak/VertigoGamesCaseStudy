using Zenject;
using UnityEngine;
using WheelReward.Reward.View;
using WheelReward.Reward.Interface;
using WheelReward.Reward.Controller;

namespace WheelReward.Reward.Installer
{
    public class MonoInstallerRewardController : MonoInstaller
    {
        [SerializeField] private RewardView rewardView;
        [SerializeField] private RewardEffect rewardEffect;

        public override void InstallBindings()
        {
            Container.Bind<IRewardEffect>()
                .FromInstance(rewardEffect)
                .AsSingle();

            Container.BindInterfacesTo<RewardController>()
                .AsSingle()
                .WithArguments(rewardView)
                .NonLazy();
        }
    }
}
