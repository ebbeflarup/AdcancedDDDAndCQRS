using System.Collections.Generic;

namespace Restaurant.Handlers
{
    public class Multiplexer<TMessage> : IHandle<TMessage>
    {
        private readonly IEnumerable<IHandle<TMessage>> _handlers;
        public Multiplexer(IEnumerable<IHandle<TMessage>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(TMessage orderPlaced)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(orderPlaced);
            }
        }
    }
}
