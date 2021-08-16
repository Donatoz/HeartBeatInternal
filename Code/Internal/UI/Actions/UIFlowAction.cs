using Bolt;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Utils;

namespace Metozis.Cardistry.Internal.UI.Actions
{
    public class UIFlowAction : IUIAction
    {
        public FlowMacro LogicMacro;
        
        public void Resolve()
        {
            var reference = GraphReference.New(LogicMacro, true);
            LogicContext.CallLogicEvent(reference, "OnResolve");
        }
    }
}