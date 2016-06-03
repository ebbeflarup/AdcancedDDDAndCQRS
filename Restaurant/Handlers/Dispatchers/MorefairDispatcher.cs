using System.Collections.Generic;
using System.Threading;
using Restaurant.Messages;

namespace Restaurant.Handlers.Dispatchers
{
    public class MorefairDispatcher<TMessage> : IHandle<TMessage> where TMessage : IMessage
    {
        private readonly IEnumerable<ThreadedHandler<TMessage>> _handlers;

        public MorefairDispatcher(IEnumerable<ThreadedHandler<TMessage>> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(TMessage orderPlaced)
        {
            while (true)
            {
                foreach (var handler in _handlers)
                {
                    if (handler.Count() < 5)
                    {
                        handler.Handle(orderPlaced);
                        return;
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
