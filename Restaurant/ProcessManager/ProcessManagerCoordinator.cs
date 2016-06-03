using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class ProcessManagerCoordinator : IHandle<OrderPlaced>
    {
        private readonly ISubscriber _subscriber;
        private IDictionary<Guid, ProcessManager> _processManagers;

        public ProcessManagerCoordinator(ISubscriber subscriber)
        {
            _subscriber = subscriber;
            _processManagers = new ConcurrentDictionary<Guid, ProcessManager>();
        }

        public void Handle(OrderPlaced orderPlaced)
        {
            var processManager = new ProcessManager();

            _subscriber.Subscribe(orderPlaced.CorrelationId, processManager);

            processManager.Handle(orderPlaced);
        }
    }
}
