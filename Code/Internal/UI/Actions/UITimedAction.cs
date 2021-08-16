using System;

namespace Metozis.Cardistry.Internal.UI.Actions
{
    public interface IUITimedAction : IUIAction
    {
        public float Duration { get; }
        public Action OnResolveEnd { get; set; }
    }
}