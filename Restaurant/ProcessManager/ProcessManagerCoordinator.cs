using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class ProcessManagerCoordinator : IHandle<OrderPlaced>
    {
        private readonly ISubscriber _subscriber;
        private readonly IPublisher _publisher;
        private IDictionary<Guid, ProcessManager> _processManagers;

        public ProcessManagerCoordinator(ISubscriber subscriber, IPublisher publisher)
        {
            _subscriber = subscriber;
            _publisher = publisher;
            _processManagers = new ConcurrentDictionary<Guid, ProcessManager>();
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            var processManager = new ProcessManager(_publisher);

            _subscriber.Subscribe(orderPlaced.CorrelationId, processManager);

            processManager.Handle(orderPlaced);
        }
    }
}
