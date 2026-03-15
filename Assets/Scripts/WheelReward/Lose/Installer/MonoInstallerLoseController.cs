using Zenject;
using UnityEngine;
using WheelReward.Lose.View;
using WheelReward.Lose.Interface;
using WheelReward.Lose.Controller;

namespace WheelReward.Lose.Installer
{
    public class MonoInstallerLoseController : MonoInstaller
    {
        [SerializeField] private LoseUI loseUI;

        public override void InstallBindings()
        {
            Container.Bind<ILoseUI>()
                .FromInstance(loseUI)
                .AsSingle();

            Container.BindInterfacesTo<LoseController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
