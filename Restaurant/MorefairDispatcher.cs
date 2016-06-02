using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Restaurant
{
    public class MorefairDispatcher : IHandle<Order>
    {
        private readonly IEnumerable<ThreadedHandler> _handlers;

        public MorefairDispatcher(IEnumerable<ThreadedHandler> handlers)
        {
            _handlers = handlers;
        }

        public void Handle(Order order)
        {
            while (true)
            {
                foreach (var handler in _handlers)
                {
                    if (handler.Count() < 5)
                    {
                        handler.Handle(order);
                        return;
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
