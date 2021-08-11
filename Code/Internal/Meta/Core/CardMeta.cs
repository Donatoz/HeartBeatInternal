using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class CardMeta : ActingEntityMeta
    {
        [TitleGroup("Card options", Alignment = TitleAlignments.Centered)]
        [ValueDropdown("GetRarities")]
        public string Rarity;

        private static IEnumerable GetRarities() => MetaManager.GetRarities();
    }

    public abstract class CardFeature
    {
        public UIObjectMetaDescriptor UIObject;
        public abstract void Resolve(Card card);
    }
}