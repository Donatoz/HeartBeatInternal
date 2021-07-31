using System;
using System.Collections;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Orders
{
    public class AsyncOrder : IOrder
    {
        public Predicate<Entity> CompleteState;
        public float StateTick = 0.03f;
        public Action<Entity, object[]> Context;
        public Action OnOrderGiven { get; set; }
        public Action OnOrderComplete { get; set; }

        public void Resolve(Entity e, object[] args)
        {
            MonoBridge.Instance.StartCoroutine(OrderRoutine(e, args));
        }

        private IEnumerator OrderRoutine(Entity e, object[] args)
        {
            if (CompleteState == null) yield break;
            
            OnOrderGiven?.Invoke();

            do
            {
                Context?.Invoke(e, args);
                yield return new WaitForSeconds(StateTick);
            }
            while (!CompleteState.Invoke(e));
            
            OnOrderComplete?.Invoke();
        }
    }
}