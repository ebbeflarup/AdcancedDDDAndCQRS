using System;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class OrderPrinter : IHandle<OrderPaid>, IHandle<OrderCooked>
    {
        public void Handle(OrderPaid orderPlaced)
        {
            PrintOrder(orderPlaced.Order);
        }

        public void Handle(OrderCooked orderPlaced)
        {
            PrintOrder(orderPlaced.Order);
        }

        private void PrintOrder(Order order)
        {
            Console.WriteLine(order.Serialize());
        }
    }
}
