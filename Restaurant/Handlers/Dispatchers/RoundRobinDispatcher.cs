using System.Collections.Generic;
using Restaurant.Messages;

namespace Restaurant.Handlers.Dispatchers
{
    public class RoundRobinDispatcher<TMessage> : IHandle<TMessage> where TMessage : IMessage
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
