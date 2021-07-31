using System;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Entities
{
    public abstract class ActingEntity : Entity, ISelectable
    {
        protected struct Validator
        {
            public Predicate<ActingEntity> Predicate;
            public string Name;
        }
        
        public Dictionary<string, Stat> Stats = new Dictionary<string, Stat>();
        public bool Selected { get; private set; }
        
        [SerializeField]
        private Player controller;
        public Player Controller
        {
            get => controller;
            set
            {
                controller = value;
                OnControllerChanged?.Invoke(value);
            }
        }

        public Action<Stat> OnStatAdded;
        public Action<bool> OnSelection;
        public Action<Player> OnControllerChanged;

        protected Action selectionPrePass;
        protected Action<ISelectable, List<ISelectable>> selectionPostPass;
        protected HashSet<Validator> validationContext;
        protected bool validated;

        public virtual Stat AddStat(string statName, Stat stat)
        {
            if (Stats.ContainsKey(statName)) return null;
            Stats[statName] = stat;
            OnStatAdded?.Invoke(stat);
            return stat;
        }

        public virtual void Select()
        {
            Selected = true;
            OnSelection?.Invoke(true);
        }

        public virtual void Deselect()
        {
            Selected = false;
            OnSelection?.Invoke(false);
        }

        protected override void Start()
        {
            
            
            base.Start();
            selectionPrePass = delegate
            {
                if (SelectionManager.Instance.SelectedObjects.Any(e =>
                    e is ActingEntity act && act.Controller != Controller))
                {
                    SelectionManager.Instance.DeselectAll();
                }
            };

            validated = Validate(false);
        }

        protected virtual void OnMouseDown()
        {
            SelectionManager.Instance.HandleSelection(
                this, 
                () => { selectionPrePass?.Invoke(); }, 
                (selectable, selected) => { selectionPostPass?.Invoke(selectable, selected);}
            );
        }

        public virtual bool Validate(bool stopOnFailure = true)
        {
            if (validationContext == null) return true;
            foreach (var validator in validationContext)
            {
                if (!validator.Predicate(this))
                {
                    Debug.Log($"{Name} failed to validate on {validator.Name}");
                    if (stopOnFailure) return false;
                }
            }
            
            return true;
        }

        public virtual void HandleAnimationEvent(string evt)
        {
            throw new NotImplementedException();
        }
    }
}