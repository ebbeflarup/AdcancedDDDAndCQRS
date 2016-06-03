using System;

namespace Restaurant.Messages.Events
{
    public class OrderPriced : MessageBase
    {
        public OrderPriced(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
