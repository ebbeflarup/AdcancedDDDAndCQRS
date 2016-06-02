using System.Collections.Generic;

namespace Restaurant
{
    public class Multiplexer<THandler, TMessage> : IHandle<THandler>
        where THandler : IHandle<TMessage>
    {
        private readonly IEnumerable<IHandle<THandler>> _handleOrders;
        public Multiplexer(IEnumerable<IHandle<THandler>> handleOrders)
        {
            _handleOrders = handleOrders;
        }

        public void Handle(THandler t)
        {
            foreach (var handleOrder in _handleOrders)
            {
                handleOrder.Handle(t);
            }
        }
    }
}
