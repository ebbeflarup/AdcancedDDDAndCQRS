using System;

namespace Restaurant
{
    public class Waitor
    {
        private readonly IHandleOrder _handleOrder;

        public Waitor(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public Guid PlaceOrder()
        {
            var newOrderGuid = Guid.NewGuid();

            //_handleOrder.Handle(new Order());

            return newOrderGuid;
        }
    }
}
