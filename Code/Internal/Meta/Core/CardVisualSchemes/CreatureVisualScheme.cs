using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Entities.Cards;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.DataHandling.Databases;
using Metozis.Cardistry.Internal.Management;
using TMPro;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes
{
    public class CreatureVisualScheme : IVisualScheme
    {
        public IEnumerable<VisualSchemeObject> CreateUI(Entity card)
        {
            var uiManager = ManagersRoot.Instance.Get<UIManager>();
            var root = card.GetModule<CardVisualModule>().Container.CardCanvas.transform.Find("Front");

            var health = new VisualSchemeObject
            {
                Bind = MonoBehaviour.Instantiate(uiManager.ObjectsCache["CardHealthStat"], root),
                UpdateContext = delegate(Entity e, GameObject bind)
                {
                    var c = e as Card;
                    bind.transform.Find("Value").GetComponent<TextMeshProUGUI>().text =
                        c.Stats[Stat.HEALTH_STAT].EffectiveValue.ToString();
                }
            };
            var attack = new VisualSchemeObject
            {
                Bind = MonoBehaviour.Instantiate(uiManager.ObjectsCache["CardAttackStat"], root),
                UpdateContext = delegate(Entity e, GameObject bind)
                {
                    var c = e as Card;
                    bind.transform.Find("Value").GetComponent<TextMeshProUGUI>().text =
                        c.Stats[Stat.ATTACK_STAT].EffectiveValue.ToString();
                }
            };
            var mana = new VisualSchemeObject
            {
                Bind = MonoBehaviour.Instantiate(uiManager.ObjectsCache["CardManaStat"], root),
                UpdateContext = delegate(Entity e, GameObject bind)
                {
                    var c = e as Card;
                    bind.transform.Find("Value").GetComponent<TextMeshProUGUI>().text =
                        c.Stats[Stat.MANA_STAT].EffectiveValue.ToString();
                }
            };
            var objectsList = new List<VisualSchemeObject>
            {
                health, attack, mana
            };
            
            if ((card as Card).MetaDescriptor.MainContext is CreatureCardContext creatureCardContext)
            {
                var unit = UnitDatabase.GetUnit(creatureCardContext.CreatureId);
                if (uiManager.UIObjectsCache.ContainsKey(unit.Meta.MainStat))
                {
                    var stat = uiManager.UIObjectsCache[unit.Meta.MainStat];
                    objectsList.Add(new VisualSchemeObject
                    {
                        Bind = uiManager.CreateUIObject(stat, root.gameObject)
                    });
                }
            }
            
            return objectsList.ToArray();
        }
    }
}