using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta
{
    [CreateAssetMenu(fileName = "New Configuration", menuName = "Metozis/Meta/Meta manager config", order = 50)]
    public class MetaManagerConfiguration : SerializedScriptableObject
    {
        public List<Rarity> Rarities;
        public List<StatGeneralMeta> Stats;
        public List<string> Attributes;
        public Dictionary<string, OrderMeta> DefaultOrders;
        
        public Rarity GetRarity(string name)
        {
            return Rarities.FirstOrDefault(r => r.Name == name);
        }
    }
    
    [Serializable]
    public class StatGeneralMeta
    {
        [Serializable]
        public class StatEfficiency
        {
            [ValueDropdown("GetStats")]
            public string AnotherStat;
            public int Efficiency;
            
            private IEnumerable GetStats()
            {
                return ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.Stats.Select(s => s.Name);
            }
        }
        
        public string Name;
        public List<StatEfficiency> Relations;
        
        [Button]
        private void InitializeRelations()
        {
            foreach (var stat in ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.Stats)
            {
                if (Relations.Find(s => s.AnotherStat == stat.Name) == null)
                {
                    Relations.Add(new StatEfficiency {AnotherStat = stat.Name, Efficiency = 1});
                }
            }
        }
    }
    
    [Serializable]
    public class AttributeGeneralMeta : NamedValue<int>
    {
        
    }
}