using System;
using System.Collections;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Reactive
{
    public class ReactiveUtils
    {
        public static void Delay(float delayTime, Action context)
        {
            MonoBridge.Instance.StartCoroutine(DelayRoutine(delayTime, context));
        }

        private static IEnumerator DelayRoutine(float delay, Action ctx)
        {
            yield return new WaitForSeconds(delay);
            ctx?.Invoke();
        }

        public static Coroutine Interval(float interval, Action ctx, Func<bool> stop)
        {
            return MonoBridge.Instance.StartCoroutine(IntervalRoutine(interval, ctx, stop));
        }
        
        private static IEnumerator IntervalRoutine(float interval, Action ctx, Func<bool> stop)
        {
            while (!stop())
            {
                ctx?.Invoke();
                yield return new WaitForSeconds(interval);
            }
        }
    }
}