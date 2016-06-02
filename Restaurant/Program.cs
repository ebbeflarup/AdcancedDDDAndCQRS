using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            var thCashier = new ThreadedHandler(new Cashier(new OrderPrinter()), "CashierThread");
            var thAssMan = new ThreadedHandler(new AssistantManager(thCashier), "AssManThread");
            var th1 = new ThreadedHandler(new Cook(thAssMan,r.Next(500, 3000), "Long"), "Long Thread");
            var th2 = new ThreadedHandler(new Cook(thAssMan, r.Next(500, 3000), "John"), "John Thread");
            var th3 = new ThreadedHandler(new Cook(thAssMan, r.Next(500, 3000), "Silver"), "Silver Thread");
            var mfth = new ThreadedHandler(new MorefairDispatcher(new List<ThreadedHandler>()
                {
                    th1, th2, th3
                }), "MoreFairThreadedHandler");
            IEnumerable<IStartable> startables = new List<IStartable> {th1, th2, th3, thAssMan, thCashier, mfth};
            IEnumerable<IMonitorable> monitorables = new List<IMonitorable> { th1, th2, th3, thAssMan, thCashier, mfth };

            var waitor = new Waitor(mfth);

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

            var timerThread = new Thread(() =>
            {
                while (true)
                {
                    foreach (var monitorable in monitorables)
                    {
                        Console.WriteLine($"Handler name {monitorable.Name} has {monitorable.Count()} orders");
                    }

                    Thread.Sleep(1000);
                }

            });

            timerThread.Start();

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
