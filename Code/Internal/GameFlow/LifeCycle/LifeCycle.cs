using System;
using Bolt;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Modularity;

namespace Metozis.Cardistry.Internal.GameFlow.LifeCycle
{
    public abstract class LifeCycle : Module
    {
        protected GraphReference logicReference;

        protected LifeCycle(Entity target, GraphReference logic, bool enabled = true) : base(target, enabled)
        {
            logicReference = logic;
        }
    }
}