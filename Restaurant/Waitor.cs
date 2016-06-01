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

        public Guid PlaceOrder(int tableNumber, LineItemList lineItemList)
        {
            var newOrderGuid = Guid.NewGuid();

            _handleOrder.Handle(new Order(newOrderGuid, tableNumber, lineItemList));

            return newOrderGuid;
        }
    }
}
