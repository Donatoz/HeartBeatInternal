using System;
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
        public string OrderName;
        public bool IsOverride;
        [ShowIf("IsOverride")]
        public FlowMacro OverrideLogic;
    }
}