using System;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    [Serializable]
    public class NamedValue<T>
    {
        public string Name;
        public T Value;
    }
}