using System;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public class GenericFlowBranch : FlowBranch
    {
        public static Func<BranchCreationContext, FlowBranch> Descriptor = context => new GenericFlowBranch(context.Parent);

        public GenericFlowBranch(FlowBranch parent) : base(parent)
        {
        }

        public override bool TakeFlow()
        {
            if (!base.TakeFlow()) return false;
            
            parent.Interrupt();
            return true;
        }

        public override bool ReturnFlow()
        {
            if (!base.ReturnFlow()) return false;
            
            parent.Resume();
            return true;
        }
    }
}