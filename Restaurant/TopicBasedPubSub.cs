using System.Collections.Generic;
using System.Linq;
using Restaurant.Messages;

namespace Restaurant
{
    public class TopicBasedPubSub : IPublisher
    {
        private readonly IDictionary<string, IList<IHandle>> _topics;
        private readonly object _topicsLock = new object();

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IList<IHandle>>();
        }

        public void Publish<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            Publish(typeof(TMessage).Name, message);
            Publish(message.CorrelationId.ToString(), message);
        }

        public void Publish<TMessage>(string topic, TMessage message)
            where TMessage : IMessage
        {
            IList<IHandle> handlers;

            if (!_topics.TryGetValue(topic, out handlers)) return;

            foreach (var typedHandler in handlers.Select(handler => handler as IHandle<TMessage>))
            {
                typedHandler?.Handle(message);
            }
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler)
            where TMessage : IMessage
        {
            Subscribe(typeof (TMessage).Name, handler);
        }

        public void Subscribe<TMessage>(string topic, IHandle<TMessage> handler)
        {
            lock (_topicsLock)
            {
                IList<IHandle> list = _topics.TryGetValue(topic, out list) ? new List<IHandle>(list) : new List<IHandle>();

                list.Add(handler);

                _topics[topic] = list;
            }
        }

        public void Unsubscribe<TMessage>(IHandle<TMessage> handler)
        {
            lock (_topicsLock)
            {
                var topic = typeof(TMessage).Name;
                var oldList = _topics[topic] ?? new List<IHandle>();
                var list = new List<IHandle>(oldList);

                list.Remove(handler);

                _topics[topic] = list;
            }
        }
    }
}