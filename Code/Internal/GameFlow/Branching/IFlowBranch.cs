using System;

namespace Metozis.Cardistry.Internal.GameFlow.Branching
{
    public interface IFlowBranch
    {
        Action OnFlowStart { get; set; }
        Action OnFlowEnd { get; set; }
        Action UpdateRequested { get; set; }

        void TakeFlow();
        void ReturnFlow();
        
        void Interrupt();
        void Resume();
    }
}