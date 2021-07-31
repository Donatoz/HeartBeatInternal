using System;
using Metozis.Cardistry.Internal.Core.Entities;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class Module
    {
        public bool Enabled { get; set; }
        protected readonly Entity target;

        public Module(Entity target, bool enabled = true)
        {
            this.target = target;
            Enabled = enabled;
        }

        public virtual void TryInvokeContext(Action ctx)
        {
            if (Enabled)
            {
                ctx.Invoke();
            }
        }
    }
}