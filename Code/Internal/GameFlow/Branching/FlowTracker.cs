using System;
using System.Collections.Generic;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public sealed class FlowTracker
    {
        public Action UpdateContext;
        public readonly IFlowBranch Main;
        
        private readonly List<IFlowBranch> subBranches = new List<IFlowBranch>();

        public FlowTracker(IFlowBranch main)
        {
            Main = main;
            main.UpdateRequested += UpdateFlow;
        }

        public void AddBranch(IFlowBranch branch)
        {
            subBranches.Add(branch);
            branch.TakeFlow();
        }

        public void RemoveBranch(IFlowBranch branch)
        {
            if (subBranches.Contains(branch))
            {
                subBranches.Remove(branch);
            }
            branch.ReturnFlow();
        }

        public void UpdateFlow()
        {
            UpdateContext?.Invoke();
        }
    }
}