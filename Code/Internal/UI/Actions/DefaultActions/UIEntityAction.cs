using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DG.Tweening;
using Metozis.Cardistry.Internal.Core.Reactive;
using Metozis.Cardistry.Internal.Core.Utils;
using Metozis.Cardistry.Internal.UI.Actions.SubActions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.UI.Actions.DefaultActions
{
    public class UIEntityAction : UITimedActionFacade, IUITimedAction
    {
        public UIEntity Target;
        public ISubAction Resolver;
        
        public float Duration => InitialDuration;
        public Action OnResolveEnd { get; set; }

        public void Resolve()
        {
            Resolver.Resolve(Target.GetComponent<UIEntity>());
            ReactiveUtils.Delay(Duration, OnResolveEnd);
        }
    }
}