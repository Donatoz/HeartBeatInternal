using UnityEngine.Events;

namespace Metozis.Cardistry.Internal.UI.Actions
{
    public class UIUnityAction : IUIAction
    {
        public UnityEvent Event;
        
        public void Resolve()
        {
            Event.Invoke();
        }
    }
}