using System.Collections.Generic;
using UnityEngine;

namespace WheelReward.Reward.Interface
{
    public interface IYourRewards
    {
        void Show(IReadOnlyDictionary<string, (Sprite sprite, int count, string name)> rewards);
    }
}
