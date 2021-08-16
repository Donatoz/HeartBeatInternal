using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace Metozis.Cardistry.Internal.UI.Events
{
    public class UIClickEvent : UIEvent, IPointerClickHandler
    {
        public bool Finite;
        [ShowIf("Finite")]
        public int ClicksAmount = -1;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (ClicksAmount == 0) return;
            
            ClicksAmount--;
            Invoke();
        }
    }
}