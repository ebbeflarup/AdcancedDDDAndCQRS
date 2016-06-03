using System;

namespace Restaurant.Messages.Commands
{
    public class RetryCookFood : MessageBase
    {
        public Order Order { get; private set; }

        public RetryCookFood(Order order, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Order = order;
        }
    }
}