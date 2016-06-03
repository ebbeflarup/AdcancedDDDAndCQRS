using Restaurant.Bus;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;
using Restaurant.Model;

namespace Restaurant.Handlers.Actors
{
    public class Cashier : IHandle<TakePayment>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(TakePayment orderPlaced)
        {
            var enrichedOrder = new Order(orderPlaced.Order.Serialize()) {Paid = true};

            _publisher.Publish(new OrderPaid(enrichedOrder, orderPlaced.CorrelationId, orderPlaced.Id));
        }
    }
}
