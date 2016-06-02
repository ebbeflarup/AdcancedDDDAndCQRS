﻿using System;
using System.Collections.Generic;

namespace Restaurant
{
    public class RoundRobinDispatcher : IHandleOrder
    {
        private readonly Queue<IHandleOrder> _handleOrders;

        public RoundRobinDispatcher(IEnumerable<IHandleOrder> handleOrders)
        {
            _handleOrders = new Queue<IHandleOrder>(handleOrders);
        }

        public void Handle(Order order)
        {
            try
            {
                _handleOrders.Peek().Handle(order);
            }
            finally
            {
                var handleOrder = _handleOrders.Dequeue();
                _handleOrders.Enqueue(handleOrder);
            }
        }
    }
}