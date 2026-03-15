using System;
using Zenject;
using UnityEngine;
using WheelReward.Signals;
using Cysharp.Threading.Tasks;
using WheelReward.Spin.Interface;
using WheelReward.Lose.Interface;
using WheelReward.Reward.Interface;
using WheelReward.Progress.Interface;

namespace WheelReward.Spin.Controller
{
    public class SpinController : ISpinStarter
    {
        private readonly SignalBus _signalBus;
        private readonly IRewardController _rewardController;
        private readonly ILoseController _loseController;
        private readonly IWheelStrategy _wheelStrategy;
        private readonly IWinSlotChooser _winSlotChooser;
        private readonly IProgressController _progressController;

        public SpinController(SignalBus signalBus, IRewardController rewardController, ILoseController loseController,
            IWheelStrategy wheelStrategy, IWinSlotChooser winSlotChooser, IProgressController progressController)
        {
            _signalBus = signalBus;
            _rewardController = rewardController;
            _loseController = loseController;
            _wheelStrategy = wheelStrategy;
            _winSlotChooser = winSlotChooser;
            _progressController = progressController;
        }

        public async void StartSpin()
        {
            try
            {
                var wheelView = _wheelStrategy.GetCurrentWheelView();
                var winSlot = _winSlotChooser.ChooseWinSlot(wheelView.SlotCount);

                _signalBus.Fire(new OnSpinStart());

                var rewardData = await wheelView.Spin(winSlot);

                if (rewardData.IsBomb)
                {
                    _loseController.Lose();
                    return;
                }

                var slotPos = wheelView.GetSlotWorldPosition(winSlot);
                _rewardController.AddReward(rewardData.Id, rewardData.Name, rewardData.Icon, rewardData.CurrenCount, slotPos).Forget();
            }
            catch (Exception e)
            {
                Debug.LogError($"SpinController: StartSpin: {e}");
            }
        }
    }
}
