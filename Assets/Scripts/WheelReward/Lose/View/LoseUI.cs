using UnityEngine;
using WheelReward.Lose.Interface;

namespace WheelReward.Lose.View
{
    public class LoseUI : MonoBehaviour, ILoseUI
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
