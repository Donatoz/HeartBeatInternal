using System;
using Metozis.Cardistry.Internal.Core.Reactive;
using UnityEngine;

namespace Metozis.Cardistry.Internal.UI.Actions.DefaultActions
{
    public class UITimedDebugAction : UITimedActionFacade, IUITimedAction
    {
        public string Message;
        public float Duration => InitialDuration;
        public Action OnResolveEnd { get; set; }
        
        public void Resolve()
        {
            OnResolveEnd += () => Debug.Log(Message);
            ReactiveUtils.Delay(Duration, () => OnResolveEnd?.Invoke());
        }
    }
}