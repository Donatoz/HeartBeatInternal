using System;
using UnityEngine;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public abstract class FlowBranch
    {
        public bool Alive { get; protected set; }
        public bool Running { get; protected set; }
        
        public Action OnFlowStart;
        public Action OnFlowEnd;

        public Action OnFlowInterrupted;
        public Action OnFlowResumed;
        
        public Action UpdateRequested;

        protected readonly FlowBranch parent;

        public FlowBranch(FlowBranch parent)
        {
            this.parent = parent;
            UpdateRequested += delegate
            {
                Debug.Log($"{GetHashCode()} updated");
            };
        }
        
        public virtual bool TakeFlow()
        {
            if (Alive) return false;
            
            Debug.Log($"{GetHashCode()} takes flow");
            Alive = true;
            Running = true;
            OnFlowStart?.Invoke();
            return true;
        }

        public virtual bool ReturnFlow()
        {
            if (!Alive) return false;
            Debug.Log($"{GetHashCode()} returns flow");

            OnFlowEnd?.Invoke();
            Alive = false;
            Running = false;
            return true;
        }

        public virtual void Resume()
        {
            Debug.Log($"{GetHashCode()} is resumed");
            Running = true;
            OnFlowResumed?.Invoke();
        }

        public virtual void Interrupt()
        {
            Debug.Log($"{GetHashCode()} is interrupted");
            Running = false;
            OnFlowInterrupted?.Invoke();
        }
    }
    
    public readonly struct BranchCreationContext
    {
        public readonly FlowBranch Parent;

        public FlowBranch ToBranch(Func<BranchCreationContext, FlowBranch> descriptor)
        {
            return descriptor.Invoke(this);
        }
    }
}