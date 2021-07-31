using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;

namespace Metozis.Cardistry.Internal.GameFlow
{
    [Serializable]
    public class Player : ActingEntity
    {
        public Player Opponent;
        public bool IsActualPlayer;
        
        public override void PopulateWithMeta(IMetaDescriptor<EntityMeta> descriptor)
        {
            throw new NotImplementedException();
        }
    }
}