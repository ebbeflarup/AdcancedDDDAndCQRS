using System;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class OrderPrinter : IHandle<OrderPaid>
    {
        public void Handle(OrderPaid orderPaid)
        {
            Console.WriteLine(orderPaid.Order.Serialize());
        }
    }
}
