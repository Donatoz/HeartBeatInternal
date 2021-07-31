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
        public Sprite CardArt;
        public Vector4 CardArtTransform;
        [ValueDropdown("GetRarities")]
        public string Rarity;

        private static IEnumerable GetRarities()
        {
            var list = new ValueDropdownList<string>();
            foreach (var rarity in ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.Rarities)
            {
                list.Add(rarity.Name, rarity.Name);
            }

            return list;
        }
    }

    public abstract class CardFeature
    {
        public UIObjectMetaDescriptor UIObject;
        public abstract void Resolve(Card card);
    }
}