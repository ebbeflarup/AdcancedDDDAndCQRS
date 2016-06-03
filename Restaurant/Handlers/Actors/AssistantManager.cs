using System.Linq;
using Restaurant.Bus;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;
using Restaurant.Model;

namespace Restaurant.Handlers.Actors
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
