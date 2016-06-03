using System;

namespace Restaurant.Messages.Commands
{
    public class CookFood : MessageBase
    {
        public Order Order { get; private set; }

        public CookFood(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }
    }
}
