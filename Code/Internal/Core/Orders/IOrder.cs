using System;
using Metozis.Cardistry.Internal.Core.Entities;

namespace Metozis.Cardistry.Internal.Core.Orders
{
    public interface IOrder
    {
        Action OnOrderGiven { get; set; }
        Action OnOrderComplete { get; set; }
        void Resolve(Entity e, params object[] args);
    }
}