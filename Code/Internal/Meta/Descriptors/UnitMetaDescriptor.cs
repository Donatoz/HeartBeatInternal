using System.Collections.Generic;
using Bolt;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using Metozis.Cardistry.Internal.Meta.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "Metozis/Unit", order = 51)]
    public class UnitMetaDescriptor : SerializedScriptableObject, IMetaDescriptor<UnitMeta>
    {
        public UnitMeta Meta;
        
        [FoldoutGroup("Descriptor")]
        public IVisualScheme VisualScheme;
        [FoldoutGroup("Descriptor")]
        public FlowMacro UnitLogicMacro;
        [FoldoutGroup("Descriptor")]
        public List<OrderSlot> AvailableOrders;
        
        [FoldoutGroup("Descriptor")]
        public List<SkillMetaDescriptor> Skills;
        [FoldoutGroup("Descriptor")]
        public List<ItemMetaDescriptor> Inventory;

        public List<IMetaFiller> Fillers;
        
        public UnitMeta GetMeta()
        {
            return Meta;
        }
        
        [Button]
        private void AddDefaultOrders()
        {
            AvailableOrders.Add(new OrderSlot{ OrderName = "UnitAttack" });
        }
    }
}