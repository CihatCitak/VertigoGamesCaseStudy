using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class OpenWheelReward : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private GameObject wheelRewardCanvas;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            wheelRewardCanvas.SetActive(true);
        }
    }
}
