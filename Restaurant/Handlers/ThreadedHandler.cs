using System.Collections.Concurrent;
using System.Threading;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class ThreadedHandler<TMessage> : IHandle<TMessage>, IStartable, IMonitorable where TMessage : IMessage
    {
        public string Name { get; }
        private readonly IHandle<TMessage> _handleOrder;
        private readonly ConcurrentQueue<TMessage> _orders; 

        public ThreadedHandler(IHandle<TMessage> handleOrder, string name)
        {
            Name = name;
            _handleOrder = handleOrder;
            _orders = new ConcurrentQueue<TMessage>();
        }

        public int Count()
        {
            return _orders.Count;
        }

        public void Start()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    TMessage order;
                    while (_orders.TryDequeue(out order))
                    {
                        _handleOrder.Handle(order);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }

        public void Handle(TMessage orderPlaced)
        {
            _orders.Enqueue(orderPlaced);
        }
    }
}
