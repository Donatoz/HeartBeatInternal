using System;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Meta.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core.CardFeatures
{
    [Serializable]
    public class ArmorModFeature : CardFeature
    {
        public int Modifier;

        public override void Resolve(Card card)
        {
            var currentHealth = card.Stats[Stat.HEALTH_STAT].BaseValue;
            card.Stats[Stat.HEALTH_STAT].BaseValueModifications.Add(i =>
                i < currentHealth ? Mathf.Max(0, i - Modifier) : i);
        }
    }
}