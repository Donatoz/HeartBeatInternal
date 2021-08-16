using System.Linq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Management;
using Sirenix.Utilities;

namespace Metozis.Cardistry.Internal.Meta.Rules.RuleSets
{
    public sealed class HeartbeatRulesSet : IRuleInjector
    {
        public void ConfigureInput()
        {
            #region Selection

            InputManager.Instance.RegisterInputAction("Fire", ctx =>
            {
                if (InputManager.Instance.State.PointedEntity == null || !(InputManager.Instance.State.PointedEntity is ISelectable))
                {
                    SelectionManager.Instance.DeselectAll();
                }
            });
            InputManager.Instance.RegisterInputAction("SecondaryFire", ctx =>
            {
                if (ctx.performed)
                {
                    SelectionManager.Instance.DeselectAll();
                }
            });

            #endregion
        }

        public void ConfigureOrders()
        {
            #region Unit orders

            SelectionManager.Instance.TypeSpecifiedPasses[typeof(Unit)] = selectable =>
            {
                // If any of selected objects is unit and it doesn't have the same owner as selected unit.
                if (SelectionManager.Instance.SelectedObjects.Any(s =>
                    s is Unit act && selectable is Unit sAct && act.Controller != sAct.Controller))
                {
                    SelectionManager.Instance.SelectedObjects
                        .Where(s => s is Unit)
                        .Cast<Unit>()
                        .ForEach(u => u.GiveOrder(u.AvailableOrders["UnitAttack"], selectable as Unit));
                    SelectionManager.Instance.DeselectAll();
                    return false;
                }

                return true;
            };

            #endregion
        }

        public void InitializeUI()
        {
            
        }

        public void InitializeUIEvents()
        {
            var skillsPanel = UIManager.Instance.EntityCache["SkillsPanel"];

            SelectionManager.Instance.OnObjectSelected += delegate
            {
                skillsPanel.InvokeAction(skillsPanel.DefaultActions["FadeIn"]);
            };
            SelectionManager.Instance.OnObjectDeselected += delegate
            {
                skillsPanel.InvokeAction(skillsPanel.DefaultActions["FadeOut"]);
            };
        }
    }
}