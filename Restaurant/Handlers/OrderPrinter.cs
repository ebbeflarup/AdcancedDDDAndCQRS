using System;
using Restaurant.Messages.Events;
using Restaurant.Model;

namespace Restaurant.Handlers
{
    public class OrderPrinter : IHandle<OrderPaid>, IHandle<OrderCooked>
    {
        public void Handle(OrderPaid orderPaid)
        {
            PrintOrder(orderPaid.Order);
        }

        public void Handle(OrderCooked orderCooked)
        {
            PrintOrder(orderCooked.Order);
        }

        private void PrintOrder(Order order)
        {
            Console.WriteLine(order.Serialize());
        }
    }
}
