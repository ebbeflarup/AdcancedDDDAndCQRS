using System;
using Restaurant.Model;

namespace Restaurant.Messages.Commands
{
    public class PriceOrder : MessageBase
    {
        public Order Order { get; private set; }

        public PriceOrder(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }
    }
}
