using System;

namespace Restaurant.Messages.Events
{
    public class OrderCooked : MessageBase
    {
        public OrderCooked(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
