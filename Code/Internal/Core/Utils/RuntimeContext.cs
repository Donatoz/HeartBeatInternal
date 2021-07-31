using Metozis.Cardistry.Internal.Core.Entities;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    public class RuntimeContext
    {
        public EntityFactory<Card> CardFactory;
        public EntityFactory<Unit> UnitFactory;

        public RuntimeContext()
        {
            CardFactory = new EntityFactory<Card>();
            UnitFactory = new EntityFactory<Unit>();
        }
    }
}