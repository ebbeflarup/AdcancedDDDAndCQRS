﻿namespace Restaurant
{
    public class Cashier : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;

        public Cashier(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public void Handle(Order order)
        { }
    }
}