using System.Collections.Generic;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Metozis.Cardistry.Internal.Meta.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    [CreateAssetMenu(fileName = "New item", menuName = "Metozis/Meta/Item")]
    public class ItemMetaDescriptor : SerializedScriptableObject, IMetaDescriptor<ItemMeta>
    {
        public ItemMeta Meta;
        [FoldoutGroup("Descriptor")]
        public int MaximumAmount;

        public ItemMeta GetMeta()
        {
            return Meta;
        }
    }
}