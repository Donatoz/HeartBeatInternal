using UnityEngine;

namespace Metozis.Cardistry.Internal.UI.Actions.DefaultActions
{
    public class UIDebugAction : IUIAction
    {
        public string Message;
        
        public void Resolve() => Debug.Log(Message);
    }
}