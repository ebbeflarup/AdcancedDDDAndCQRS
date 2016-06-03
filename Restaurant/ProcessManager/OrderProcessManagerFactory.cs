namespace Restaurant.ProcessManager
{
    public class OrderProcessManagerFactory
    {
        private readonly IPublisher _publisher;
        public OrderProcessManagerFactory(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public IOrderProcessManager CreateOrderProcessManager(Order order)
        {
            if (order.IsDodgy)
                return new PaymentFirstOrderProcessmanager(_publisher);

            return new OrderProcessManager(_publisher);
        }
    }
}
