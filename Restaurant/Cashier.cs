namespace Restaurant
{
    public class Cashier : IHandleOrder
    {
        private readonly IPublisher _publisher;

        public Cashier(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(Order order)
        {
            var enrichedOrder = new Order(order.Serialize()) {Paid = true};

            _publisher.Publish("orderPaid", enrichedOrder);
        }
    }
}
