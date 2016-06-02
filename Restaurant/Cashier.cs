using Restaurant.Messages.Events;

namespace Restaurant
{
    public class Cashier : IHandle<OrderPriced>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderPriced orderPriced)
        {
            var enrichedOrder = new Order(orderPriced.Order.Serialize()) {Paid = true};

            _publisher.Publish(new OrderPaid(enrichedOrder));
        }
    }
}
