using System.Collections.Generic;
using Metozis.Cardistry.Internal.Core.Orders;

namespace Metozis.Cardistry.Internal.Core.Interaction
{
    public interface IControllable : ISelectable
    {
        void GiveOrder(IOrder order, params object[] args);
        Dictionary<string, IOrder> AvailableOrders { get; }
    }
}