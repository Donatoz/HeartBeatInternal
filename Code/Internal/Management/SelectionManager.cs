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

        public readonly Dictionary<Type, TypeSpecifiedPass> TypeSpecifiedPasses =
            new Dictionary<Type, TypeSpecifiedPass>();
        
        private List<ISelectable> selected = new List<ISelectable>();
        private List<ISelectable> previousSelected;
        
        public IReadOnlyList<ISelectable> SelectedObjects => selected;

        public Action<ISelectable> OnObjectSelected;
        public Action<ISelectable> OnObjectDeselected;
        public Action OnDeselectedAll;

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
            selected.ForEach(s =>
            {
                s.Deselect();
                OnObjectDeselected?.Invoke(s);
            });
            selected.Clear();
            OnDeselectedAll?.Invoke();
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