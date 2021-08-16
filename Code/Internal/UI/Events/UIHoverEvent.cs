using UnityEngine.EventSystems;

namespace Metozis.Cardistry.Internal.UI.Events
{
    public class UIHoverEvent : UIEvent, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Invoke();
        }
    }
}