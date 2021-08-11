using System.Collections.Generic;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Runtime;

namespace Metozis.Cardistry.Internal.Meta.Descriptors
{
    public interface IMetaDescriptor<out T> where T : EntityMeta
    {
        T GetMeta();
    }
}