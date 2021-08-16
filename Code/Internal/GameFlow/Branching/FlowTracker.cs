using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public sealed class FlowTracker
    {
        public readonly FlowBranch Main;
        
        private readonly List<FlowBranch> subBranches = new List<FlowBranch>();

        public FlowTracker(FlowBranch main)
        {
            Main = main;
            Main.TakeFlow();
        }

        public void AddBranch(FlowBranch branch)
        {
            subBranches.Add(branch);
        }

        public void RemoveBranch(FlowBranch branch)
        {
            if (subBranches.Contains(branch))
            {
                subBranches.Remove(branch);
            }
        }

        public void UpdateFlow()
        {
            if (Main.Running)
            {
                Main.UpdateRequested?.Invoke();
            }
            subBranches.Where(b => b.Alive && b.Running).ForEach(b => b.UpdateRequested?.Invoke());
        }
    }
}