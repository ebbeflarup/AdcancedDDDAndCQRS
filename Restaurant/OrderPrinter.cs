using System;

namespace Restaurant
{
    public class OrderPrinter : IHandleOrder
    {
        public void Handle(Order order)
        {
            Console.WriteLine(order.Serialize());
        }
    }
}
