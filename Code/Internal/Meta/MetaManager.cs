﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta
{
    public class MetaManager : SerializedMonoBehaviour
    {
        public static MetaManager Instance => ManagersRoot.Instance.Get<MetaManager>();
        
        public MetaManagerConfiguration Configuration;
        
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
    }
}