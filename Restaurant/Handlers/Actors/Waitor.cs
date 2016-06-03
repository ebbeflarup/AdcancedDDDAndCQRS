using System;
using Restaurant.Bus;
using Restaurant.Messages.Events;
using Restaurant.Model;

namespace Restaurant.Handlers.Actors
{
    public class Waitor
    {
        private readonly IPublisher _publisher;
        private bool _isDodgy = true;

        public Waitor(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public Guid PlaceOrder(int tableNumber, LineItemList lineItemList)
        {
            var newOrderGuid = Guid.NewGuid();
            
            _isDodgy = !_isDodgy;

            _publisher.Publish(new OrderPlaced(new Order(newOrderGuid, tableNumber, lineItemList) {IsDodgy = _isDodgy}, Guid.NewGuid(), Guid.Empty));

            return newOrderGuid;
        }
    }
}
