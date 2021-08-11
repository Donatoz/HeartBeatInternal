using System;
using System.Collections;
using Bolt;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Sirenix.OdinInspector;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class OrderMeta
    {
        public FlowMacro OrderLogicMacro;
        public TargetValidator Validator;
    }
    
    [Serializable]
    public class OrderSlot
    {
        [ValueDropdown("GetAvailableOrders")]
        public string OrderName;
        public bool IsOverride;
        [ShowIf("IsOverride")]
        public FlowMacro OverrideLogic;

        private IEnumerable GetAvailableOrders() => MetaManager.GetOrders();
    }
}