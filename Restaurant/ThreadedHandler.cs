using System.Collections.Concurrent;
using System.Threading;

namespace Restaurant
{
    public class ThreadedHandler<T> : IHandle<T>, IStartable, IMonitorable
    {
        public string Name { get; }
        private readonly IHandle<T> _handleOrder;
        private readonly ConcurrentQueue<T> _orders; 

        public ThreadedHandler(IHandle<T> handleOrder, string name)
        {
            Name = name;
            _handleOrder = handleOrder;
            _orders = new ConcurrentQueue<T>();
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
                    T order;
                    while (_orders.TryDequeue(out order))
                    {
                        _handleOrder.Handle(order);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }

        public void Handle(T orderPlaced)
        {
            _orders.Enqueue(orderPlaced);
        }
    }
}
