using Zenject;
using UnityEngine;
using WheelReward.Progress.View;
using WheelReward.Progress.Controller;

namespace WheelReward.Progress.Installer
{
    public class MonoInstallerProgressController : MonoInstaller
    {
        [SerializeField] private ProgressBar progressBar;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProgressController>()
                .AsSingle()
                .WithArguments(progressBar)
                .NonLazy();
        }
    }
}