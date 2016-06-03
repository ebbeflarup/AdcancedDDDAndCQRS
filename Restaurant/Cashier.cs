using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class Cashier : IHandle<TakePayment>
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(TakePayment takePayment)
        {
            var enrichedOrder = new Order(takePayment.Order.Serialize()) {Paid = true};

            _publisher.Publish(new OrderPaid(enrichedOrder));
        }
    }
}
