using System.Threading;
using Restaurant.Messages.Events;

namespace Restaurant
{
    public class Cook : IHandle<OrderPlaced>
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

        public void Handle(OrderPlaced orderPlaced)
        {
            var enrichedOrder = new Order(orderPlaced.Order.Serialize()) {Ingredients = "ponies, elephants"};
            Thread.Sleep(_sleeptime);

            _publisher.Publish(new OrderCooked(enrichedOrder));
        }
    }
}
