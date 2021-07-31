using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core.Units;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class UnitMeta : ActingEntityMeta
    {
        [ValueDropdown("GetStats")]
        public string MainStat;
        [ValueDropdown("GetRarities")]
        public string Rarity;

        public Sprite Art;
        public Vector4 ArtTransform;

        public List<SkillMeta> Skills;
    
        private IEnumerable GetStats => MetaManager.GetStats();
        private IEnumerable GetRarities => MetaManager.GetRarities();
    }
}