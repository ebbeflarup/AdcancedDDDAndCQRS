using System.Collections.Concurrent;
using System.Threading;

namespace Restaurant
{
    public class ThreadedHandler : IHandleOrder, IStartable, IMonitorable
    {
        public string Name { get; }
        private readonly IHandleOrder _handleOrder;
        private readonly ConcurrentQueue<Order> _orders; 

        public ThreadedHandler(IHandleOrder handleOrder, string name)
        {
            Name = name;
            _handleOrder = handleOrder;
            _orders = new ConcurrentQueue<Order>();
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
                    Order order;
                    while (_orders.TryDequeue(out order))
                    {
                        _handleOrder.Handle(order);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }

        public void Handle(Order order)
        {
            _orders.Enqueue(order);
        }
    }
}
