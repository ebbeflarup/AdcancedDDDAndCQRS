using System.Collections.Generic;

namespace Restaurant
{
    public class RoundRobinDispatcher<TMessage> : IHandle<TMessage>
    {
        private readonly Queue<IHandle<TMessage>> _handlers;

        public RoundRobinDispatcher(IEnumerable<IHandle<TMessage>> handlers)
        {
            _handlers = new Queue<IHandle<TMessage>>(handlers);
        }

        public void Handle(TMessage orderPlaced)
        {
            try
            {
                _handlers.Peek().Handle(orderPlaced);
            }
            finally
            {
                var handler = _handlers.Dequeue();
                _handlers.Enqueue(handler);
            }
        }
    }
}
