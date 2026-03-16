using TMPro;
using UnityEngine;
using WheelReward.Info.Interface;

namespace WheelReward.Info.View
{
    public class InfoView : MonoBehaviour, IInfoView
    {
        [SerializeField] private TextMeshProUGUI nextSafeStageText;
        [SerializeField] private TextMeshProUGUI finalStageText;
        
        [SerializeField] private string safeAreaText = "Safe Area"; 
        [SerializeField] private string finalAreaText = "Final Area"; 

        public void SetNextSafeStage(int stage) => nextSafeStageText.text = $"{safeAreaText} {stage}";
        public void SetFinalStage(int stage) => finalStageText.text = $"{finalAreaText} {stage}";
    }
}
