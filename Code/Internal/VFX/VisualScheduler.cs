using System;
using System.Collections;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.VFX
{
    public class VisualScheduler : MonoBehaviour
    {
        public static VisualScheduler Instance => ManagersRoot.Instance.Get<VisualScheduler>();
        
        private readonly Queue<VFXScheduleContext> schedule = new Queue<VFXScheduleContext>();

        public Action<VFXScheduleContext> OnVFXQueued;

        private void FixedUpdate()
        {
            if (schedule.Count == 0) return;
            var currentContext = schedule.Peek();

            if (!currentContext.Entity.Started)
            {
                currentContext.Entity.Place();
            }
            
            if (currentContext.Entity.Complete)
            {
                Destroy(currentContext.Entity);
                schedule.Dequeue();
            }
        }

        public void Schedule(VFXScheduleContext context)
        {
            if (context.Instant)
            {
                context.Entity.Place();
                return;
            }
            schedule.Enqueue(context);
            OnVFXQueued?.Invoke(context);
        }
    }

    public struct VFXScheduleContext
    {
        public VFXEntity Entity;
        public bool Instant;
    }
}