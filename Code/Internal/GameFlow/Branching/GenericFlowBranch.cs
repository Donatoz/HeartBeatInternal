using System;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public abstract class GenericFlowBranch : IFlowBranch
    {
        public Action OnFlowStart { get; set; }
        public Action OnFlowEnd { get; set; }
        public Action UpdateRequested { get; set; }

        protected readonly IFlowBranch parent;
        
        public GenericFlowBranch(IFlowBranch parent)
        {
            this.parent = parent;
        }
        
        public void TakeFlow()
        {
            parent.Interrupt();
            OnFlowStart?.Invoke();
        }

        public void ReturnFlow()
        {
            OnFlowEnd?.Invoke();
            parent.Resume();
        }

        public virtual void Resume()
        {
            OnFlowStart?.Invoke();
        }

        public virtual void Interrupt()
        {
            OnFlowEnd?.Invoke();
        }
    }
}