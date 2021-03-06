﻿using System;
using System.Collections.Generic;
using System.Threading;
using Restaurant.Bus;
using Restaurant.Handlers;
using Restaurant.Handlers.Actors;
using Restaurant.Handlers.Dispatchers;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;
using Restaurant.Model;
using Restaurant.ProcessManager;

namespace Restaurant
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = new TopicBasedPubSub();
            var r = new Random();
            var timedHandlerTh = new TimedHandler(bus);
            var thCashier = new ThreadedHandler<TakePayment>(new Cashier(bus), "Cashier");
            var thAssMan = new ThreadedHandler<PriceOrder>(new AssistantManager(bus), "AssMan");
            var th1 = new ThreadedHandler<CookFood>(new Cook(bus, r.Next(500, 3000), "Cook1"), "Cook1");
            var th2 = new ThreadedHandler<CookFood>(new Cook(bus, r.Next(500, 3000), "Cook2"), "Cook2");
            var th3 = new ThreadedHandler<CookFood>(new Cook(bus, r.Next(500, 3000), "Cook3"), "Cook3");
            var mfth =
                new ThreadedHandler<CookFood>(
                    new MoreFairDispatcher<CookFood>(new List<ThreadedHandler<CookFood>>
                    {
                        th1,
                        th2,
                        th3
                    }), "MoreFair");
            var processManagerCoorTh = new ThreadedHandler<OrderPlaced>(new OrderProcessManagerCoordinator(bus, bus), "ProcessManagerCoordinator");
            IEnumerable<IStartable> startables = new List<IStartable> {th1, th2, th3, thAssMan, thCashier, mfth, processManagerCoorTh, timedHandlerTh };
            IEnumerable<IMonitorable> monitorables = new List<IMonitorable> { th1, th2, th3, thAssMan, thCashier, mfth, processManagerCoorTh };

            var waitor = new Waitor(bus);

            // Wiring
            bus.Subscribe(new DroppingHandler<CookFood>(mfth));
            bus.Subscribe(thAssMan);
            bus.Subscribe(thCashier);
            bus.Subscribe<OrderPaid>(new OrderPrinter());
            //bus.Subscribe<OrderCooked>(new OrderPrinter());
            //bus.Subscribe(Guid.NewGuid(), new Monitor());
            bus.Subscribe(timedHandlerTh);
            bus.Subscribe(processManagerCoorTh);


            foreach (var startable in startables)
            {
                startable.Start();
            }

            for (var i = 0; i < 20; i++)
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
                        Console.WriteLine($"{monitorable.Name} has {monitorable.Count} orders");
                    }

                    Thread.Sleep(1000);
                }

            });
            timerThread.Start();

            Console.ReadLine();
        }
    }
}
