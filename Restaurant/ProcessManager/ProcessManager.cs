using System;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public class ProcessManager : IHandle<OrderPlaced>, IHandle<OrderCooked>
    {
        public void Handle(OrderPlaced orderPlaced)
        {
            Console.WriteLine("OrderPlaced");
        }

        public void Handle(OrderCooked orderCooked)
        {
            Console.WriteLine("OrderCooked");
        }
    }
}
