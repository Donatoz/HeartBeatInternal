using System;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Utils
{
    public struct TypeValidator
    {
        public TypesTuple TypesTuple;

        public bool Validate(Type type)
        {
            return TypesTuple.GetTypes().Contains(type);
        }
    }
}