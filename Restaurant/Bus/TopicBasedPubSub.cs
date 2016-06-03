﻿using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Handlers;
using Restaurant.Messages;

namespace Restaurant.Bus
{
    public class TopicBasedPubSub : IPublisher, ISubscriber
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

        private void Publish<TMessage>(string topic, TMessage message)
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

        private void Subscribe(string topic, IHandle handler)
        {
            lock (_topicsLock)
            {
                IList<IHandle> list = _topics.TryGetValue(topic, out list) ? new List<IHandle>(list) : new List<IHandle>();

                list.Add(handler);

                _topics[topic] = list;
            }
        }

        public void Subscribe(Guid correlationId, IHandle handler)
        {
            Subscribe(correlationId.ToString(), handler);
        }

        public void Unsubscribe<TMessage>(IHandle<TMessage> handler)
        {
            Unsubscribe(typeof(TMessage).Name, handler);
        }

        public void Unsubscribe(Guid correlationId, IHandle handler)
        {
            Unsubscribe(correlationId.ToString(), handler);
        }

        private void Unsubscribe(string topic, IHandle handler)
        {
            lock (_topicsLock)
            {
                var oldList = _topics[topic] ?? new List<IHandle>();
                var list = new List<IHandle>(oldList);

                list.Remove(handler);

                _topics[topic] = list;
            }
        }
    }
}