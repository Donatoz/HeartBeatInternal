using System;
using Bolt;

namespace Metozis.Cardistry.Internal.Meta.Core.Units
{
    [Serializable]
    public class SkillMeta : ActingEntityMeta
    {
        public TargetValidator Targets;
        public FlowMacro SkillLogicMacro;
    }
}