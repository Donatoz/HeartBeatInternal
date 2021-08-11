using System;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public abstract class ParallelFlowBranch : IFlowBranch
    {
        public Action OnFlowStart { get; set; }
        public Action OnFlowEnd { get; set; }
        public Action UpdateRequested { get; set; }
        
        protected readonly IFlowBranch parent;

        public ParallelFlowBranch(IFlowBranch parent)
        {
            this.parent = parent;
        }
        
        public void TakeFlow()
        {
            OnFlowStart?.Invoke();
        }

        public void ReturnFlow()
        {
            OnFlowEnd?.Invoke();
        }

        public abstract void Interrupt();
        public abstract void Resume();
    }
}