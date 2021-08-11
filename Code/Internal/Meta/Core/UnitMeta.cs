using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class UnitMeta : ActingEntityMeta
    {
        [TitleGroup("Unit options", Alignment = TitleAlignments.Centered)]
        [ValueDropdown("GetStats")]
        public string MainStat;
        [TitleGroup("Unit options", Alignment = TitleAlignments.Centered)]
        [ValueDropdown("GetRarities")]
        public string Rarity;

        private IEnumerable GetStats => MetaManager.GetStats();
        private IEnumerable GetRarities => MetaManager.GetRarities();
    }
}