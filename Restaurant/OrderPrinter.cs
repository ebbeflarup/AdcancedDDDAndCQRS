using System;

namespace Restaurant
{
    public class OrderPrinter : IHandle<Order>
    {
        public void Handle(Order order)
        {
            Console.WriteLine(order.Serialize());
        }
    }
}
