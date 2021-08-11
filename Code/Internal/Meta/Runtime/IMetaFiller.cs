using Metozis.Cardistry.Internal.Core.Entities;

namespace Metozis.Cardistry.Internal.Meta.Runtime
{
    public interface IMetaFiller
    {
        void Populate(Entity entity);
    }
}