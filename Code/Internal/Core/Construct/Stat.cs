using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Reactive;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Construct
{
    public class Stat
    {
        #region Constants

        public const string HEALTH_STAT = "Health";
        public const string ATTACK_STAT = "Attack";
        public const string MANA_STAT = "Mana";

        #endregion

        private int baseValue;
        
        #region Public_Members

        /// <summary>
        /// Base value of the stat.
        /// </summary>
        public int BaseValue
        {
            get => baseValue;
            set
            {
                var finalVal = BaseValueModifications.Aggregate(value, (current, modification) => modification(current));
                baseValue = Mathf.Clamp(finalVal, MinimumValue, MaximumValue);
                OnValueChanged?.Invoke();
            } 
        }

        /// <summary>
        /// Minimal value which stat can have.
        /// </summary>
        public int MinimumValue { get; set; }
        
        /// <summary>
        /// Maximal value which stat can have.
        /// </summary>
        public int MaximumValue { get; set; }
        
        /// <summary>
        /// Base value summed with all modifiers.
        /// </summary>
        public int EffectiveValue
        {
            get
            {
                var result = BaseValue;
                for (var i = 0; i < modifiers.Count; i++)
                {
                    result += modifiers[i].Value;
                }

                return result;
            }
        }
        
        #endregion
        
        #region Private_Members
        
        /// <summary>
        /// All stat-affecting modifiers.
        /// </summary>
        private IList<Modifier> modifiers;

        #endregion

        #region Events
        /// <summary>
        /// Delegate for all events connected to stat (value) changes.
        /// </summary>
        public delegate void StateChanged();
        
        /// <summary>
        /// Invokes when stat effective/base value changes (new modifier being added).
        /// </summary>
        public event StateChanged OnValueChanged;
        
        #endregion

        public List<Func<int, int>> BaseValueModifications = new List<Func<int, int>>();
        
        public Stat(int baseValue, int minimumValue = Int32.MinValue, int maximumValue = Int32.MaxValue)
        {
            MinimumValue = minimumValue;
            MaximumValue = maximumValue;
            BaseValue = baseValue;
            modifiers = new List<Modifier>();
        }
        
        /// <summary>
        /// Modify stat by adding new modifier.
        /// </summary>
        /// <param name="modifier">New modifier</param>
        public void AddModifier(Modifier modifier)
        {
            modifiers.Add(modifier);
            OnValueChanged?.Invoke();
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Stat stat)) return false;

            return stat.EffectiveValue == EffectiveValue
                   && stat.modifiers.Count == modifiers.Count
                   && BaseValue == stat.BaseValue;
        }
        
        /// <summary>
        /// Remove modifier from stat.
        /// </summary>
        /// <param name="modifier">Existing modifier</param>
        public void RemoveModifier(Modifier modifier)
        {
            modifiers.Remove(modifier);
            OnValueChanged?.Invoke();
        }
    }
}