using System;
using System.Collections.Generic;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var thAssMan = new ThreadedHandler(new AssistantManager(new Cashier(new OrderPrinter())));
            var th1 = new ThreadedHandler(new Cook(thAssMan));
            var th2 = new ThreadedHandler(new Cook(thAssMan));
            var th3 = new ThreadedHandler(new Cook(thAssMan));
            IEnumerable<IStartable> startables = new List<IStartable> {th1, th2, th3, thAssMan};

            var cookRoundRobinDispatcher =
                new RoundRobinDispatcher(new List<ThreadedHandler>()
                {
                    th1, th2, th3
                });
            var waitor = new Waitor(cookRoundRobinDispatcher);

            foreach (var startable in startables)
            {
                startable.Start();
            }

            for (int i = 0; i < 20; i++)
            {
                var lineItems = new LineItemList
                {
                    new LineItem(2, "Ice Cream", 2.99),
                    new LineItem(2, "Burger", 4.99)
                };
                waitor.PlaceOrder(2, lineItems);
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
