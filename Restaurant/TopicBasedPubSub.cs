using System.Collections.Generic;

namespace Restaurant
{
    public class TopicBasedPubSub : IPublisher
    {
        private readonly IDictionary<string, IHandle<Order>> _topics;

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IHandle<Order>>();
        }

        public void Publish(string topic, Order order)
        {
            IHandle<Order> orderHandler;
            if (_topics.TryGetValue(topic, out orderHandler))
                orderHandler.Handle(order);
        }

        public void Subscribe(string topic, IHandle<Order> orderHandler)
        {
            _topics.Add(topic, orderHandler);
        }
    }
}
