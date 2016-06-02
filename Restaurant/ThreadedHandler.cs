using System.Collections.Concurrent;
using System.Threading;

namespace Restaurant
{
    public class ThreadedHandler : IHandleOrder, IStartable
    {
        private readonly IHandleOrder _handleOrder;
        private readonly ConcurrentQueue<Order> _orders; 

        public ThreadedHandler(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
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
