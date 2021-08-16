using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Metozis.Cardistry.Internal.UI.Actions.SubActions
{
    public class FadeSubAction : ISubAction
    {
        public float TargetOpacity;
        public float EffectiveDuration;

        private static Dictionary<Type, Action<UIEntity, float, float>> processors =
            new Dictionary<Type, Action<UIEntity, float, float>>
            {
                {
                    typeof(CanvasGroup),
                    delegate(UIEntity entity, float opacity, float duration)
                    {
                        var canvasGroup = entity.GetComponent<CanvasGroup>();
                        canvasGroup.DOFade(opacity, duration);
                    }
                }
            };
        
        public virtual void Resolve(UIEntity target)
        {
            if (target == null) return;
            
            foreach (var key in processors.Keys)
            {
                if (target.GetComponent(key) != null)
                {
                    processors[key].Invoke(target, TargetOpacity, EffectiveDuration);
                    break;
                }
            }
        }
    }
}