using System.Threading;

namespace Restaurant
{
    public class Cook : IHandleOrder
    {
        public string Name { get; }
        private readonly IHandleOrder _orderHandler;
        private readonly int _sleeptime;

        public Cook(IHandleOrder orderHandler, int sleeptime, string name)
        {
            Name = name;
            _orderHandler = orderHandler;
            _sleeptime = sleeptime;
        }

        public void Handle(Order order)
        {
            var enrichedOrder = new Order(order.Serialize()) {Ingredients = "ponies, elephants"};
            Thread.Sleep(_sleeptime);

            _orderHandler.Handle(enrichedOrder);
        }
    }
}
