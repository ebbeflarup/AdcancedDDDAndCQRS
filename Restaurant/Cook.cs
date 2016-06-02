using System.Threading;

namespace Restaurant
{
    public class Cook : IHandle<Order>
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

        public void Handle(Order order)
        {
            var enrichedOrder = new Order(order.Serialize()) {Ingredients = "ponies, elephants"};
            Thread.Sleep(_sleeptime);

            _publisher.Publish("orderCooked", enrichedOrder);
        }
    }
}
