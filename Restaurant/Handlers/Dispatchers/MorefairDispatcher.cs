using System.Collections.Generic;
using System.Linq;
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

        public void Handle(TMessage message)
        {
            while (true)
            {
                var handlerWithNonFullQueue = _handlers.FirstOrDefault(h => h.Count < 5);

                if (handlerWithNonFullQueue != null)
                {
                    handlerWithNonFullQueue.Handle(message);
                    return;
                }
                Thread.Sleep(1);
            }
        }
    }
}
