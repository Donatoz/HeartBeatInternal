using Metozis.Cardistry.Internal.Meta.Core;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    public interface IMetaDescriptor<out T> where T : EntityMeta
    {
        T GetMeta(); 
    }
}