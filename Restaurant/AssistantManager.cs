using System.Linq;
using Restaurant.Messages;
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

        public void Handle(PriceOrder priceOrder)
        {
            var enrichedOrder = new Order(priceOrder.Order.Serialize())
            {
                Total = priceOrder.Order.LineItems.Sum(lineItem => lineItem.Price),
                Tax = 6.99
            };

            _publisher.Publish(new OrderPriced(enrichedOrder));
        }
    }
}
