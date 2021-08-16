using System.Collections.Generic;
using Metozis.Cardistry.Internal.UI.Actions;
using Sirenix.OdinInspector;

namespace Metozis.Cardistry.Internal.UI.Events
{
    public class UIEventReceiver : SerializedMonoBehaviour
    {
        public Dictionary<string, List<IUIAction>> EventBindings;
        
        public void Receive(string eventName)
        {
            if (!EventBindings.ContainsKey(eventName)) return;
            
            EventBindings[eventName].ForEach(action => action.Resolve());
        }
    }
}