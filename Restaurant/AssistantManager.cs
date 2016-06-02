using System.Linq;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class AssistantManager : IHandle<OrderCooked>
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(OrderCooked orderCooked)
        {
            var enrichedOrder = new Order(orderCooked.Order.Serialize())
            {
                Total = orderCooked.Order.LineItems.Sum(lineItem => lineItem.Price),
                Tax = 6.99
            };

            _publisher.Publish(new OrderPriced(enrichedOrder));
        }
    }
}
