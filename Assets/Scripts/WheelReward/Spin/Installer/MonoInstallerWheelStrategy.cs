using Zenject;
using UnityEngine;
using WheelReward.Spin.View;
using WheelReward.Spin.Model;
using WheelReward.Spin.Controller;

namespace WheelReward.Spin.Installer
{
    public class MonoInstallerWheelStrategy : MonoInstaller
    {
        [SerializeField] private WheelView bronzeWheelView;
        [SerializeField] private WheelView silverWheelView;
        [SerializeField] private WheelView goldWheelView;
        [SerializeField] private WheelRewardConfig bronzeRewardConfig;
        [SerializeField] private WheelRewardConfig silverRewardConfig;
        [SerializeField] private WheelRewardConfig goldRewardConfig;

        public override void InstallBindings()
        {
            var arguments = new object[]
            {
                bronzeWheelView, silverWheelView, goldWheelView,
                bronzeRewardConfig, silverRewardConfig, goldRewardConfig
            };

            Container.BindInterfacesTo<WheelStrategyController>()
                .AsSingle()
                .WithArguments(arguments)
                .NonLazy();
        }
    }
}
