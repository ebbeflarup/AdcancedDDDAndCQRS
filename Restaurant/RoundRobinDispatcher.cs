using System.Collections.Generic;

namespace Restaurant
{
    public class RoundRobinDispatcher : IHandle<Order>
    {
        private readonly Queue<IHandle<Order>> _handleOrders;

        public RoundRobinDispatcher(IEnumerable<IHandle<Order>> handleOrders)
        {
            _handleOrders = new Queue<IHandle<Order>>(handleOrders);
        }

        public void Handle(Order order)
        {
            try
            {
                _handleOrders.Peek().Handle(order);
            }
            finally
            {
                var handleOrder = _handleOrders.Dequeue();
                _handleOrders.Enqueue(handleOrder);
            }
        }
    }
}
