using System.Collections.Generic;

namespace Restaurant
{
    public class TopicBasedPubSub : IPublisher
    {
        private readonly IDictionary<string, IHandleOrder> _topics;

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IHandleOrder>();
        }

        public void Publish(string topic, Order order)
        {
            IHandleOrder orderHandler;
            if (_topics.TryGetValue(topic, out orderHandler))
                orderHandler.Handle(order);
        }

        public void Subscribe(string topic, IHandleOrder orderHandler)
        {
            _topics.Add(topic, orderHandler);
        }
    }
}
