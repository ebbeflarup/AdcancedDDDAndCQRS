using System.Linq;

namespace Restaurant
{
    public class AssistantManager : IHandleOrder
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(Order order)
        {
            var enrichedOrder = new Order(order.Serialize())
            {
                Total = order.LineItems.Sum(lineItem => lineItem.Price),
                Tax = 6.99
            };

            _publisher.Publish("orderPriced", enrichedOrder);
        }
    }
}
