using Zenject;
using UnityEngine;
using WheelReward.Spin.View;
using WheelReward.Spin.Controller;
using WheelReward.Spin.Model;

namespace WheelReward.Spin.Installer
{
    public class MonoInstallerSpinController : MonoInstaller
    {
        [SerializeField] private WheelView wheelView;
        [SerializeField] private WheelRewardConfig wheelRewardConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SpinController>()
                .AsSingle()
                .WithArguments(wheelView, wheelRewardConfig)
                .NonLazy();
        }
    }
}