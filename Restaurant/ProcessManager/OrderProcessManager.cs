using System;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class OrderProcessManager : IOrderProcessManager, IHandle<OrderCooked>, IHandle<OrderPriced>, IHandle<OrderPaid>, IHandle<RetryCookFood>
    {
        private readonly IPublisher _publisher;
        private bool _isFoodCooked;

        public Action<Guid> OnCompleted { get; set; }

        public OrderProcessManager(IPublisher publisher)
        {
            _publisher = publisher;
            _isFoodCooked = false;
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("OrderPlaced");
            _publisher.Publish(new CookFood(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id));
            _publisher.Publish(new SendToMeIn(10, new RetryCookFood(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id), orderPlaced.CorrelationId, orderPlaced.CausationId));
        }

        public void Handle(OrderCooked orderCooked)
        {
            Console.WriteLine("OrderCooked");
            _isFoodCooked = true;
            _publisher.Publish(new PriceOrder(orderCooked.Order, orderCooked.CorrelationId, orderCooked.Id));
        }

        public void Handle(OrderPriced orderPriced)
        {
            Console.WriteLine("OrderPriced");
            _publisher.Publish(new TakePayment(orderPriced.Order, orderPriced.CorrelationId, orderPriced.Id));
        }

        public void Handle(OrderPaid orderPaid)
        {
            Console.WriteLine("OrderPaid");
            OnCompleted(orderPaid.CorrelationId);
        }

        public void Handle(RetryCookFood orderPlaced)
        {
            if (_isFoodCooked)
                return;

            _publisher.Publish(new CookFood(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.CausationId));
        }
    }
}
