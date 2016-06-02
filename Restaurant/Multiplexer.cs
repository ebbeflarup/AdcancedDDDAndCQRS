using System.Collections.Generic;

namespace Restaurant
{
    public class Multiplexer : IHandleOrder
    {
        private readonly IEnumerable<IHandleOrder> _handleOrders;
        public Multiplexer(IEnumerable<IHandleOrder> handleOrders)
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
