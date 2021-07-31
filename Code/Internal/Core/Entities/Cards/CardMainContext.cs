using System;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;

namespace Metozis.Cardistry.Internal.Core.Entities.Cards
{
    public interface ICardMainContext
    {
        void InvokePlayContext(Card host);
    }
}