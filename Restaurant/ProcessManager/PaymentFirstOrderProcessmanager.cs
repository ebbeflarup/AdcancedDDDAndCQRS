﻿using System;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class PaymentFirstOrderProcessmanager : IOrderProcessManager, IHandle<OrderCooked>, IHandle<OrderPriced>, IHandle<OrderPaid>
    {
        private readonly IPublisher _publisher;

        public Action<Guid> OnCompleted { get; set; }

        public PaymentFirstOrderProcessmanager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("OrderPlaced");
            _publisher.Publish(new PriceOrder(orderPlaced.Order, orderPlaced.CorrelationId, orderPlaced.Id));
        }

        public void Handle(OrderCooked orderCooked)
        {
            Console.WriteLine("OrderCooked");
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
        }
    }
}