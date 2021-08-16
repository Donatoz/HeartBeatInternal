using Ludiq;
using Metozis.Cardistry.Internal.Core.Utils;
using Metozis.Cardistry.Internal.Meta.Core.Units;

namespace Metozis.Cardistry.Internal.Core.Entities.Units
{
    public class Skill
    {
        private readonly SkillMeta meta;
        private readonly Unit owner;
        private GraphReference logicRef;
        
        public Skill(Unit owner, SkillMeta meta)
        {
            this.meta = meta;
            this.owner = owner;
            logicRef = GraphReference.New(meta.SkillLogicMacro, true);
        }
        
        public void Cast()
        {
            LogicContext.CallLogicEvent(logicRef, "OnCast", new []{owner});
        }
    }
}