using System;
using Metozis.Cardistry.Internal.Core.Entities;

namespace Metozis.Cardistry.Internal.Meta.Core
{
    public class CreationPass
    {
        public string PrefabPath;
        public Action<Entity> PrePass;
        public Action<Entity> PostPass;
    }
}