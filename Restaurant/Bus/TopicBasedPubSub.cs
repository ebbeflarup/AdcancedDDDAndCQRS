using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Handlers;
using Restaurant.Messages;

namespace Restaurant.Bus
{
    public class TopicBasedPubSub : IPublisher, ISubscriber
    {
        private readonly IDictionary<string, IList<dynamic>> _topics;
        private readonly object _topicsLock = new object();

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IList<dynamic>>();
        }

        public void Publish<TMessage>(TMessage message)
            where TMessage : IMessage
        {
            Publish(typeof(TMessage).Name, message);
            Publish(message.CorrelationId.ToString(), message);
        }

        private void Publish<TMessage>(string topic, TMessage message)
            where TMessage : IMessage
        {
            IList<dynamic> handlers;

            // ReSharper disable once InconsistentlySynchronizedField
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

        private void Subscribe<TMessage>(string topic, IHandle<TMessage> handler) where TMessage : IMessage
        {
            lock (_topicsLock)
            {
                IList<dynamic> list = _topics.TryGetValue(topic, out list) ? new List<dynamic>(list) : new List<dynamic>();

                list.Add(handler);

                _topics[topic] = list;
            }
        }

        public void Subscribe<TMessage>(Guid correlationId, IHandle<TMessage> handler) where TMessage : IMessage
        {
            Subscribe(correlationId.ToString(), handler);
        }

        public void Unsubscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage
        {
            Unsubscribe(typeof(TMessage).Name, handler);
        }

        public void Unsubscribe<TMessage>(Guid correlationId, IHandle<TMessage> handler) where TMessage : IMessage
        {
            Unsubscribe(correlationId.ToString(), handler);
        }

        private void Unsubscribe<TMessage>(string topic, IHandle<TMessage> handler) where TMessage : IMessage
        {
            lock (_topicsLock)
            {
                var oldList = _topics[topic] ?? new List<dynamic>();
                var list = new List<dynamic>(oldList);

                list.Remove(handler);

                _topics[topic] = list;
            }
        }
    }
}