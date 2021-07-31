using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class ActingEntityMeta : EntityMeta
    {
        public List<StatMeta> Stats;
    }
    
    [Serializable]
    public class StatMeta
    {
        [ValueDropdown("GetStats")]
        public string StatName;
        public int CurrentValue;
        [HideInInspector]
        public List<ModifierMeta> Modifiers;
        public bool HasUI = true;

        public Vector2Int MinMax;
        
        private IEnumerable GetStats => MetaManager.GetStats();
    }
    
    [Serializable]
    public class ModifierMeta
    {
        public string Id;
        public int Value;
    }
}