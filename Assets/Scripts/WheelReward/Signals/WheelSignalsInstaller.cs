using Zenject;

namespace WheelReward.Signals
{
    public class WheelSignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<OnSpinAvailable>();
        }
    }
}