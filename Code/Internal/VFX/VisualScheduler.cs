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
        
        private readonly Queue<ScheduleContext> schedule = new Queue<ScheduleContext>();

        public Action<ScheduleContext> OnVFXQueued;

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

        public void Schedule(ScheduleContext context)
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

    public struct ScheduleContext
    {
        public VFXEntity Entity;
        public bool Instant;
    }
}