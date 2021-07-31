using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core.CardVisualSchemes;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Metozis.Cardistry.Internal.Meta.Core.UnitVisualSchemes
{
    public class UnitDefaultVisualScheme : IVisualScheme
    {
        private UIManager uiManager => ManagersRoot.Instance.Get<UIManager>();
        private SessionManager sessionManager => ManagersRoot.Instance.Get<SessionManager>();

        private void UpdateSelectionEffect(Unit unit)
        {
            var selectionEffect = unit.GetModule<UnitVisualModule>().Container.UnitCanvas.transform.Find("SelectionEffect");
            selectionEffect.GetComponent<Image>().color = unit.Controller == sessionManager.ActualPlayer
                ? uiManager.AllyUnitColor
                : uiManager.EnemyUnitColor;
            selectionEffect.GetComponentsInChildren<Image>().ForEach(i => i.color =
                unit.Controller == sessionManager.ActualPlayer
                    ? uiManager.AllyUnitColor
                    : uiManager.EnemyUnitColor);
        }

        public IEnumerable<VisualSchemeObject> CreateUI(Entity unit)
        {
            var root = unit.GetModule<UnitVisualModule>().Container.UnitCanvas.transform.Find("Front");
            var acting = unit as ActingEntity;
            
            unit.OnInitialized += delegate { UpdateSelectionEffect(unit as Unit); };
            acting.OnControllerChanged += delegate { UpdateSelectionEffect(unit as Unit); };

            var health = new VisualSchemeObject
            {
                Bind = MonoBehaviour.Instantiate(uiManager.ObjectsCache["UnitHealthStat"], root),
                UpdateContext = delegate(Entity e, GameObject bind)
                {
                    var u = e as Unit;
                    bind.transform.Find("Value").GetComponent<Text>().text =
                        u.Stats[Stat.HEALTH_STAT].EffectiveValue.ToString();
                }
            };
            var attack = new VisualSchemeObject
            {
                Bind = MonoBehaviour.Instantiate(uiManager.ObjectsCache["UnitAttackStat"], root),
                UpdateContext = delegate(Entity e, GameObject bind)
                {
                    var u = e as Unit;
                    bind.transform.Find("Value").GetComponent<Text>().text =
                        u.Stats[Stat.ATTACK_STAT].EffectiveValue.ToString();
                }
            };

            return new []{health, attack};
        }
    }
}