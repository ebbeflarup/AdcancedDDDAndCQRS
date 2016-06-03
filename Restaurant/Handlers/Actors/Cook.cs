using System.Threading;
using Restaurant.Bus;
using Restaurant.Messages.Commands;
using Restaurant.Messages.Events;
using Restaurant.Model;

namespace Restaurant.Handlers.Actors
{
    public class Cook : IHandle<CookFood>
    {
        public string Name { get; }
        private readonly IPublisher _publisher;
        private readonly int _sleeptime;

        public Cook(IPublisher publisher, int sleeptime, string name)
        {
            Name = name;
            _publisher = publisher;
            _sleeptime = sleeptime;
        }

        public void Handle(CookFood orderPlaced)
        {
            var enrichedOrder = new Order(orderPlaced.Order.Serialize()) {Ingredients = "ponies, elephants"};
            Thread.Sleep(_sleeptime);

            _publisher.Publish(new OrderCooked(enrichedOrder, orderPlaced.CorrelationId, orderPlaced.Id));
        }
    }
}
