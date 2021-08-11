using System;
using System.Collections;
using System.Collections.Generic;
using Metozis.Cardistry.Code.External.Editor.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class ActingEntityMeta : EntityMeta
    {
        [TitleGroup("Acting entity options", Alignment = TitleAlignments.Centered)]
        public List<StatMeta> Stats;
        [TitleGroup("Acting entity options", Alignment = TitleAlignments.Centered)]
        public Sprite Art;
        [TitleGroup("Acting entity options", Alignment = TitleAlignments.Centered)]
        public Vector4 ArtTransform;
        
        [TitleGroup("Acting entity options", Alignment = TitleAlignments.Centered)]
        [Button(ButtonSizes.Large)]
        private void AddCustomStat()
        {
            var stat = new StatMeta();
            AddStatWindow.Open(stat, Stats);
        }
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