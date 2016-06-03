using System;
using System.Collections.Generic;
using System.Threading;
using Restaurant.Messages.Events;

namespace Restaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var bus = new TopicBasedPubSub();
            var r = new Random();
            var thCashier = new ThreadedHandler<OrderPriced>(new Cashier(bus), "CashierThread");
            var thAssMan = new ThreadedHandler<OrderCooked>(new AssistantManager(bus), "AssManThread");
            var th1 = new ThreadedHandler<OrderPlaced>(new Cook(bus, r.Next(500, 3000), "Long"), "Long Thread");
            var th2 = new ThreadedHandler<OrderPlaced>(new Cook(bus, r.Next(500, 3000), "John"), "John Thread");
            var th3 = new ThreadedHandler<OrderPlaced>(new Cook(bus, r.Next(500, 3000), "Silver"), "Silver Thread");
            var mfth = new ThreadedHandler<OrderPlaced>(new MorefairDispatcher<OrderPlaced>(new List<ThreadedHandler<OrderPlaced>>()
                {
                    th1, th2, th3
                }), "MoreFairThreadedHandler");
            IEnumerable<IStartable> startables = new List<IStartable> {th1, th2, th3, thAssMan, thCashier, mfth};
            IEnumerable<IMonitorable> monitorables = new List<IMonitorable> { th1, th2, th3, thAssMan, thCashier, mfth };

            var waitor = new Waitor(bus);

            // Wiring
            bus.Subscribe( mfth);
            bus.Subscribe(thAssMan);
            bus.Subscribe(thCashier);
            bus.Subscribe<OrderPaid>(new OrderPrinter());
            bus.Subscribe<OrderCooked>(new OrderPrinter());


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
