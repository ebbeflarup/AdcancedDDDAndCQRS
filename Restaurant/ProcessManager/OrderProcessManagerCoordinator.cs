using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class OrderProcessManagerCoordinator : IHandle<OrderPlaced>
    {
        private readonly ISubscriber _subscriber;
        private readonly IPublisher _publisher;
        private readonly IDictionary<Guid, IOrderProcessManager> _processManagers;
        private readonly OrderProcessManagerFactory _orderProcessManagerFactory;

        public OrderProcessManagerCoordinator(ISubscriber subscriber, IPublisher publisher)
        {
            _subscriber = subscriber;
            _publisher = publisher;
            _processManagers = new ConcurrentDictionary<Guid, IOrderProcessManager>();
            _orderProcessManagerFactory = new OrderProcessManagerFactory(publisher);
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            var processManager = _orderProcessManagerFactory.CreateOrderProcessManager(orderPlaced.Order);
            processManager.OnCompleted = OnCompleted;
            _processManagers.Add(orderPlaced.CorrelationId, processManager);

            _subscriber.Subscribe(orderPlaced.CorrelationId, processManager);

            processManager.Handle(orderPlaced);
        }

        private void OnCompleted(Guid correlationId)
        {
            _processManagers.Remove(correlationId);
        }
    }
}
