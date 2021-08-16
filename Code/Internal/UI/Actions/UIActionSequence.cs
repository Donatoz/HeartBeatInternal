using System.Collections.Generic;

namespace Metozis.Cardistry.Internal.UI.Actions
{
    public class UIActionSequence : IUIAction
    {
        public List<IUITimedAction> Actions;
        public bool Parallel;
        
        public void Resolve()
        {
            if (Parallel)
            {
                Actions.ForEach(action => action.Resolve());
            }
            else
            {
                for (var i = 0; i < Actions.Count; i++)
                {
                    if (i == Actions.Count - 1) break;
                    Actions[i].OnResolveEnd += Actions[i + 1].Resolve;
                }
            
                Actions[0].Resolve();
            }
        }
    }
}