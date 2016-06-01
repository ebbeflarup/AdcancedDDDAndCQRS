using System.Linq;

namespace Restaurant
{
    public class AssistantManager : IHandleOrder
    {
        private readonly IHandleOrder _handleOrder;

        public AssistantManager(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }

        public void Handle(Order order)
        {
            var enrichedOrder = new Order(order.Serialize())
            {
                Total = order.LineItems.Sum(lineItem => lineItem.Price),
                Tax = 6.99
            };

            _handleOrder.Handle(enrichedOrder);
        }
    }
}
