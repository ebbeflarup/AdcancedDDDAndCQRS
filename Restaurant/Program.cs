using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var assistantManager = new AssistantManager(new Cashier(new OrderPrinter()));
            var cookMultiplexer =
                new Multiplexer(new List<Cook>()
                {
                    new Cook(assistantManager),
                    new Cook(assistantManager),
                    new Cook(assistantManager)
                });

            var cookRoundRobinDispatcher =
                new RoundRobinDispatcher(new List<Cook>()
                {
                    new Cook(assistantManager),
                    new Cook(assistantManager),
                    new Cook(assistantManager)
                });
            var waitor = new Waitor(cookRoundRobinDispatcher);

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
