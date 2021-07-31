using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using Sirenix.Utilities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class AnimationModule : Module
    {
        private Animator animator;
        
        public Action<string> OnAnimationPlay;
        public Action<string> OnAnimationEnd;

        public Dictionary<string, Action> AnimationEvents = new Dictionary<string, Action>();
        
        public AnimationModule(Entity target, bool enabled = true) : base(target, enabled)
        {
            animator = target.GetComponent<Animator>();
        }

        public void PlayAnimation(string trigger, bool children = false)
        {
            OnAnimationPlay?.Invoke(trigger);
            animator.SetTrigger(trigger);

            if (children)
            {
                target.GetComponentsInChildren<Animator>().ForEach(a => a.SetTrigger(trigger));
            }
        }

        public void HandleAnimationEvent(string evt)
        {
            if (AnimationEvents.ContainsKey(evt))
            {
                AnimationEvents[evt].Invoke();
            }
        }
    }
}