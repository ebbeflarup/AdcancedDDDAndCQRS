using System;
using Restaurant.Model;

namespace Restaurant.Messages.Events
{
    public class OrderPaid : MessageBase
    {
        public OrderPaid(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
