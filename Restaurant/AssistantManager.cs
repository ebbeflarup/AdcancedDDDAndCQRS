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
            var enrichedOrder = new Order(order.Serialize());

            

            _handleOrder.Handle(enrichedOrder);
        }
    }
}
