using System;
using System.Collections.Generic;
using Bolt;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Utils;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Orders
{
    public class FlowOrder : IOrder
    {
        public Action OnOrderGiven { get; set; }
        public Action OnOrderComplete { get; set; }

        private readonly GraphReference logicRef;
        
        public FlowOrder(FlowMacro context)
        {
            logicRef = GraphReference.New(context, true);
            
            OnOrderGiven += delegate
            {
                LogicContext.CallLogicEvent(logicRef, "OnOrderGiven");
            };
            
            OnOrderComplete += delegate
            {
                LogicContext.CallLogicEvent(logicRef, "OnOrderComplete");
            };
        }

        public void Resolve(Entity e, params object[] args)
        {
            var extArgs = new List<object> {e};
            extArgs.AddRange(args);

            OnOrderGiven.Invoke();
            LogicContext.CallLogicEvent(logicRef, "OnContext", extArgs.ToArray());
            OnOrderComplete.Invoke();
        }
    }
}