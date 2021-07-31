using System;
using Bolt;
using Ludiq;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    public static class LogicContext
    {
        public static void CallLogicEvent(GraphReference r, string eventName, object[] args = null)
        {
            r.TriggerEventHandler(
                hook => hook.name == "Custom", 
                args == null ? new CustomEventArgs(eventName) : new CustomEventArgs(eventName, args), 
                parent => true, 
                true);
        }
    }
}