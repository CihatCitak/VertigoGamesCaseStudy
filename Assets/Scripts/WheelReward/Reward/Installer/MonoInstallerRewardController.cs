using Zenject;
using UnityEngine;
using WheelReward.Reward.View;
using WheelReward.Reward.Controller;

namespace WheelReward.Reward.Installer
{
    public class MonoInstallerRewardController : MonoInstaller
    {
        [SerializeField] private RewardView rewardView;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<RewardController>()
                .AsSingle()
                .WithArguments(rewardView)
                .NonLazy();
        }
    }
}