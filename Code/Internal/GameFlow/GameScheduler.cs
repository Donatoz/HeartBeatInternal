using System;
using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Orders;
using UnityEngine;

namespace Metozis.Cardistry.Internal.GameFlow
{
    public sealed class GameScheduler
    {
        private readonly Queue<ScheduleContext> schedule = new Queue<ScheduleContext>();
        
        public void Schedule(ScheduleContext context)
        {
            schedule.Enqueue(context);
            Refresh();
        }

        public void Refresh()
        {
            if (schedule.Count == 0) return;
            var ctx = schedule.Peek();

            if (ctx.Started) return;
            
            ctx.OnFinish += delegate
            {
                schedule.Dequeue();
                Refresh();
            };
            
            ctx.Run();
        }
    }

    public class ScheduleContext
    {
        public Action Context;
        public Action OnFinish;
        public Action OnStart;
        
        public bool Started { get; protected set; }
        public bool Complete { get; protected set; }

        public ScheduleContext()
        {
            OnStart += delegate
            {
                Started = true;
            };
            OnFinish += delegate
            {
                Complete = true;
            };
        }

        public virtual void Run()
        {
            if (Started) return;
            
            OnStart.Invoke();
            Context?.Invoke();
            OnFinish.Invoke();
        }

        public static ScheduleContext FromOrder(IOrder order, Entity e, params object[] args)
        {
            var ctx = new ScheduleContext
            {
                Context = delegate { order.Resolve(e, args); }
            };
            if (order.OnOrderComplete != null)
            {
                ctx.OnFinish += order.OnOrderComplete;
            }

            if (order.OnOrderGiven != null)
            {
                ctx.OnStart += order.OnOrderGiven;
            }

            return ctx;
        }
    }
}