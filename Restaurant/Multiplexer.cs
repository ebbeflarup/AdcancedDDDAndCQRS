using System.Collections.Generic;

namespace Restaurant
{
    public class Multiplexer : IHandle<Order>
    {
        private readonly IEnumerable<IHandle<Order>> _handleOrders;
        public Multiplexer(IEnumerable<IHandle<Order>> handleOrders)
        {
            _handleOrders = handleOrders;
        }

        public void Handle(Order order)
        {
            foreach (var handleOrder in _handleOrders)
            {
                handleOrder.Handle(order);
            }
        }
    }
}
