using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Handlers;
using Restaurant.Messages;

namespace Restaurant.Bus
{
    public class TopicBasedPubSub : IPublisher, ISubscriber
    {
        private readonly IDictionary<string, IList<dynamic>> _subscriptions;
        private readonly object _subscriptionsLock = new object();

        public TopicBasedPubSub()
        {
            _subscriptions = new Dictionary<string, IList<dynamic>>();
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
            IList<dynamic> handlersSubscribedToTopic;

            // ReSharper disable once InconsistentlySynchronizedField
            if (!_subscriptions.TryGetValue(topic, out handlersSubscribedToTopic)) return;

            foreach (var handlerOfMessageSubscribedToTopic in handlersSubscribedToTopic.Where(handler => handler is IHandle<TMessage>))
            {
                handlerOfMessageSubscribedToTopic.Handle(message);
            }
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler)
            where TMessage : IMessage
        {
            Subscribe(typeof (TMessage).Name, handler);
        }

        private void Subscribe<TMessage>(string topic, IHandle<TMessage> handler) where TMessage : IMessage
        {
            lock (_subscriptionsLock)
            {
                IList<dynamic> handlersSubscribedToTopic = _subscriptions.TryGetValue(topic, out handlersSubscribedToTopic)
                    ? new List<dynamic>(handlersSubscribedToTopic)
                    : new List<dynamic>();

                handlersSubscribedToTopic.Add(handler);

                _subscriptions[topic] = handlersSubscribedToTopic;
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
            lock (_subscriptionsLock)
            {
                IList<dynamic> handlersSubscribedToTopic = _subscriptions.TryGetValue(topic, out handlersSubscribedToTopic)
                    ? new List<dynamic>(handlersSubscribedToTopic)
                    : new List<dynamic>();

                handlersSubscribedToTopic.Remove(handler);

                _subscriptions[topic] = handlersSubscribedToTopic;
            }
        }
    }
}