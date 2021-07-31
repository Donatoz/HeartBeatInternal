using System.Collections.Generic;
using Bolt;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Metozis/Unit", order = 51)]
    public class UnitMetaDescriptor : SerializedScriptableObject, IMetaDescriptor<UnitMeta>
    {
        public UnitMeta Meta;
        public IVisualScheme VisualScheme;
        public FlowMacro UnitLogicMacro;
        public List<OrderSlot> AvailableOrders;

        public UnitMeta GetMeta()
        {
            return Meta;
        }
    }
}