using Zenject;
using UnityEngine;
using WheelReward.Progress.View;
using WheelReward.Progress.Model;
using WheelReward.Progress.Controller;

namespace WheelReward.Progress.Installer
{
    public class MonoInstallerProgressController : MonoInstaller
    {
        [SerializeField] private ProgressBar progressBar;
        [SerializeField] private ProgressBarConfig progressBarConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ProgressController>()
                .AsSingle()
                .WithArguments(progressBar, progressBarConfig)
                .NonLazy();
        }
    }
}
