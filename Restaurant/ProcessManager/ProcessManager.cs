using System;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class ProcessManager : IHandle<OrderPlaced>, IHandle<OrderCooked>, IHandle<OrderPriced>
    {
        private readonly IPublisher _publisher;

        public ProcessManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("OrderPlaced");
            _publisher.Publish(new CookFood(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id));
        }

        public void Handle(OrderCooked orderCooked)
        {
            Console.WriteLine("OrderCooked");
            _publisher.Publish(new PriceOrder(orderCooked.Order, orderCooked.CorrelationId, orderCooked.Id));
        }

        public void Handle(OrderPriced orderPlaced)
        {
            Console.WriteLine("OrderPriced");
            _publisher.Publish(new TakePayment(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id));
        }
    }
}
