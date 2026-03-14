using Zenject;
using WheelReward.Spin.Controller;

namespace WheelReward.Spin.Installer
{
    public class MonoInstallerSpinController : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SpinController>()
                .AsSingle()
                .NonLazy();
        }
    }
}
