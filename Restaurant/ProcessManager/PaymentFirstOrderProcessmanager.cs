using System;
using Restaurant.Bus;
using Restaurant.Handlers;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class PaymentFirstOrderProcessmanager : IOrderProcessManager, IHandle<OrderCooked>, IHandle<OrderPriced>, IHandle<OrderPaid>, IHandle<RetryCookFood>
    {
        private readonly IPublisher _publisher;

        public Action<Guid> OnCompleted { get; set; }

        private bool _isFoodCooked;

        public PaymentFirstOrderProcessmanager(IPublisher publisher)
        {
            _publisher = publisher;
            _isFoodCooked = false;
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("OrderPlaced");
            _publisher.Publish(new PriceOrder(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id));
        }

        public void Handle(OrderCooked orderCooked)
        {
            Console.WriteLine("OrderCooked");
            _isFoodCooked = true;
            OnCompleted(orderCooked.CorrelationId);
        }

        public void Handle(OrderPriced orderPriced)
        {
            Console.WriteLine("OrderPriced");
            _publisher.Publish(new TakePayment(orderPriced.Order, orderPriced.CorrelationId, orderPriced.Id));
        }

        public void Handle(OrderPaid orderPaid)
        {
            Console.WriteLine("OrderPaid");
            _publisher.Publish(new CookFood(orderPaid.Order, orderPaid.CorrelationId, orderPaid.Id));
            _publisher.Publish(new SendToMeIn(10, new RetryCookFood(orderPaid.Order, orderPaid.CorrelationId, orderPaid.Id), orderPaid.CorrelationId, orderPaid.CausationId));
        }

        public void Handle(RetryCookFood orderPlaced)
        {
            if (_isFoodCooked)
                return;

            _publisher.Publish(new CookFood(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.CausationId));
        }
    }
}
