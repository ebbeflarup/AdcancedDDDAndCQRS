using System.Collections.Generic;

namespace Restaurant
{
    public class RoundRobinDispatcher<THandler, TMessage> : IHandle<THandler>
        where THandler : IHandle<TMessage>
    {
        private readonly Queue<IHandle<THandler>> _handleOrders;

        public RoundRobinDispatcher(IEnumerable<IHandle<THandler>> handleOrders)
        {
            _handleOrders = new Queue<IHandle<THandler>>(handleOrders);
        }

        public void Handle(THandler t)
        {
            try
            {
                _handleOrders.Peek().Handle(t);
            }
            finally
            {
                var handleOrder = _handleOrders.Dequeue();
                _handleOrders.Enqueue(handleOrder);
            }
        }
    }
}
