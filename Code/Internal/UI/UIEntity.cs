using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.UI.Actions;
using Metozis.Cardistry.Internal.UI.Actions.DefaultActions;
using Metozis.Cardistry.Internal.UI.Actions.SubActions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.UI
{
    public class UIEntity : SerializedMonoBehaviour
    {
        public string BindId;
        public readonly Dictionary<string, UIEntityAction> DefaultActions = new Dictionary<string, UIEntityAction>();

        protected virtual void Awake()
        {
            UIManager.Instance.RegisterEntity(this);
        }

        protected virtual void OnDestroy()
        {
            UIManager.Instance.ForgetEntity(this);
        }
        
        public void InvokeAction(UIEntityAction action)
        {
            action.Target = this;
            action.Resolve();
        }
        
        [Button]
        private void InitializeDefaultActions()
        {
            DefaultActions["FadeIn"] = new UIEntityAction
            {
                InitialDuration = 1,
                Resolver = new FadeSubAction
                {
                    EffectiveDuration = 1,
                    TargetOpacity = 1
                }
            };
            DefaultActions["FadeOut"] = new UIEntityAction
            {
                InitialDuration = 1,
                Resolver = new FadeSubAction
                {
                    EffectiveDuration = 1,
                    TargetOpacity = 0
                }
            };
        }
    }
}