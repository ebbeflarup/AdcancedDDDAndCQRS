using System;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class Waitor
    {
        private readonly IPublisher _publisher;

        public Waitor(IPublisher publisher)
        {
            _publisher = publisher;

        }

        public Guid PlaceOrder(int tableNumber, LineItemList lineItemList)
        {
            var newOrderGuid = Guid.NewGuid();

            _publisher.Publish(new OrderPlaced(new Order(newOrderGuid, tableNumber, lineItemList), Guid.NewGuid(), Guid.Empty));

            return newOrderGuid;
        }
    }
}
