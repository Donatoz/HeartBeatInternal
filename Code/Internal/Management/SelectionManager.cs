using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class SelectionManager : MonoBehaviour
    {
        public delegate bool TypeSpecifiedPass(ISelectable selectable);
        public static SelectionManager Instance => ManagersRoot.Instance.Get<SelectionManager>();

        public readonly Dictionary<Type, TypeSpecifiedPass> TypeSpecifiedPasses = new Dictionary<Type, TypeSpecifiedPass>
        {
            {
                typeof(Unit),
                selectable =>
                {
                    if (Instance.SelectedObjects.Any(s =>
                        s is Unit act && selectable is Unit sAct && act.Controller != sAct.Controller))
                    {
                        Instance.SelectedObjects
                            .Where(s => s is Unit)
                            .Cast<Unit>()
                            .ForEach(u => u.GiveOrder(u.AvailableOrders["UnitAttack"], selectable as Unit));
                        Instance.DeselectAll();
                        return false;
                    }
                    return true;
                }
            }
        };
        
        private List<ISelectable> selected = new List<ISelectable>();
        private List<ISelectable> previousSelected;
        
        public IReadOnlyList<ISelectable> SelectedObjects => selected;

        public Action<ISelectable> OnObjectSelected;

        private void Awake()
        {
            InputManager.Instance.RegisterInputAction("Fire", ctx =>
            {
                if (InputManager.Instance.State.PointedEntity == null || !(InputManager.Instance.State.PointedEntity is ISelectable))
                {
                    DeselectAll();
                }
            });
            InputManager.Instance.RegisterInputAction("SecondaryFire", ctx =>
            {
                if (ctx.performed)
                {
                    DeselectAll();
                }
            });
        }
        
        public void HandleSelection(ISelectable selectable, Action prePass = null, Action<ISelectable, List<ISelectable>> postPass = null)
        {
            if (selected.Contains(selectable)) return;
            if (TypeSpecifiedPasses.ContainsKey(selectable.GetType()))
            {
                if (!TypeSpecifiedPasses[selectable.GetType()].Invoke(selectable))
                {
                    return;
                }
            }
            
            prePass?.Invoke();
            
            selected.Add(selectable);
            selectable.Select();
            OnObjectSelected?.Invoke(selectable);
            
            postPass?.Invoke(selectable, selected);
        }
        
        public void DeselectAll()
        {
            previousSelected = new List<ISelectable>(selected);
            selected.ForEach(s => s.Deselect());
            selected.Clear();
        }

        public void RevertDeselect()
        {
            if (previousSelected != null)
            {
                selected.ForEach(s => s.Deselect());
                selected = new List<ISelectable>(previousSelected);
                selected.ForEach(s => s.Select());
            }
        }
    }
}