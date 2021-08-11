using System.Collections.Generic;
using Bolt;
using Metozis.Cardistry.Extensions;
using Metozis.Cardistry.Internal.Core.Entities.Cards;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Core.CardFeatures;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using Metozis.Cardistry.Internal.Meta.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Metozis/Card", order = 50)]
    public class CardMetaDescriptor : SerializedScriptableObject, IMetaDescriptor<CardMeta>
    {
        public CardMeta Meta;
        [FoldoutGroup("Descriptor")]
        public ICardMainContext MainContext;
        [FoldoutGroup("Descriptor")]
        public IVisualScheme VisualScheme;
        [FoldoutGroup("Descriptor")]
        public List<CardFeature> Features;
        [FoldoutGroup("Descriptor")]
        public FlowMacro CardLogicMacro;
        
        public CardMeta GetMeta()
        {
            return Meta;
        }

        [Title("Default Features", "Choose feature to add", TitleAlignments.Centered)]
        [ShowInInspector]
        [ReadOnly]
        private string Entity = "Card";
        
        [ButtonGroup("Features")]
        [Button("Weapon Modifier")]
        private void AddWeaponMod()
        {
            Features.Add(new WeaponModFeature());
        }
        
        [ButtonGroup("Features")]
        [Button("Armor Modifier")]
        private void AddArmorMod()
        {
            Features.Add(new ArmorModFeature());
        }
    }
}