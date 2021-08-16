using System;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public class AsyncFlowBranch : FlowBranch
    {
        public static Func<BranchCreationContext, FlowBranch> Descriptor = context => new AsyncFlowBranch(context.Parent);
        
        public readonly FlowBranch Origin;
        
        public AsyncFlowBranch(FlowBranch parent) : base(parent)
        {
            Origin = parent;
        }
    }
}