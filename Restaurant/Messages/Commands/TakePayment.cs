using System;
using Restaurant.Model;

namespace Restaurant.Messages.Commands
{
    public class TakePayment : MessageBase
    {
        public Order Order { get; private set; }

        public TakePayment(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }
    }
}
