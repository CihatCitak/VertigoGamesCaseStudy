using Zenject;
using UnityEngine;
using WheelReward.Info.View;
using WheelReward.Info.Controller;

namespace WheelReward.Info.Installer
{
    public class MonoInstallerInfoController : MonoInstaller
    {
        [SerializeField] private InfoView infoView;

        public override void InstallBindings()
        {

            Container.BindInterfacesTo<InfoController>()
                .AsSingle()
                .WithArguments(infoView)
                .NonLazy();
        }
    }
}
