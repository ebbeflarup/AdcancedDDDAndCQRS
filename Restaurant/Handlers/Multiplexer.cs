using System.Collections.Generic;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class Multiplexer<TMessage> : IHandle<TMessage> where TMessage : IMessage
    {
        private readonly IEnumerable<IHandle<TMessage>> _handlers;

        public Multiplexer(IEnumerable<IHandle<TMessage>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(TMessage message)
        {
            foreach (var handler in _handlers)
            {
                handler.Handle(message);
            }
        }
    }
}
