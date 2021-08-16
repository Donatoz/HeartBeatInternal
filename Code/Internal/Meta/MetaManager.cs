using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Rules;
using Metozis.Cardistry.Internal.Meta.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta
{
    public class MetaManager : SerializedMonoBehaviour
    {
        public static MetaManager Instance => ManagersRoot.Instance.Get<MetaManager>();
        public IRuleInjector Rules;
        
        public MetaManagerConfiguration Configuration;
        public ColorScheme MainColorScheme;
        
        public static IEnumerable<string> GetStats()
        {
            var list = new List<string>();
            list.Add("Health");
            list.Add("Attack");
            list.Add("Mana");
            list.AddRange(ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.Stats.Select(s => s.Name));
            return list;
        }

        public static IEnumerable<string> GetRarities()
        {
            return ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.Rarities.Select(r => r.Name);
        }

        public static IEnumerable<string> GetOrders()
        {
            return ManagersRoot.Instance.GetComponent<MetaManager>().Configuration.DefaultOrders.Keys;
        }

        private void Start()
        {
            LoadRules(Rules);
        }

        private void LoadRules(IRuleInjector rules)
        {
            rules.ConfigureInput();
            rules.ConfigureOrders();
            
            rules.InitializeUI();
            rules.InitializeUIEvents();
        }
    }
}