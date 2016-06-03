using System.Linq;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class AssistantManager : IHandle<PriceOrder>
    {
        private readonly IPublisher _publisher;

        public AssistantManager(IPublisher publisher)
        {
            _publisher = publisher;
        }

        public void Handle(PriceOrder orderPlaced)
        {
            var enrichedOrder = new Order(orderPlaced.Order.Serialize())
            {
                Total = orderPlaced.Order.LineItems.Sum(lineItem => lineItem.Price),
                Tax = 6.99
            };

            _publisher.Publish(new OrderPriced(enrichedOrder, orderPlaced.CorrelationId, orderPlaced.Id));
        }
    }
}
