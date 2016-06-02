using System;

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

            _publisher.Publish("OrderCreated", new Order(newOrderGuid, tableNumber, lineItemList));

            return newOrderGuid;
        }
    }
}
