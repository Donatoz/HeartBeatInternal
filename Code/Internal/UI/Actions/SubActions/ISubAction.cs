using UnityEngine;

namespace Metozis.Cardistry.Internal.UI.Actions.SubActions
{
    public interface ISubAction
    {
        void Resolve(UIEntity target);
    }
}