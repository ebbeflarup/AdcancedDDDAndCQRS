﻿using System.Collections.Generic;
using Restaurant.Messages;

namespace Restaurant
{
    public class TopicBasedPubSub : IPublisher
    {
        private readonly IDictionary<string, IList<IHandle>> _topics;

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IList<IHandle>>();
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler)
            where TMessage : IMessage
        {
            Subscribe(typeof(TMessage).Name, handler);
        }

        public void Subscribe<TMessage>(string topic, IHandle<TMessage> handler)
        {
            var oldList = _topics[topic] ?? new List<IHandle>();

            _topics[topic] = new List<IHandle>(oldList) { handler };
        }

        public void Unsubscribe<TMessage>(IHandle<TMessage> handler)
        {
            var topic = typeof (TMessage).Name;
            var oldList = _topics[topic] ?? new List<IHandle>();
            var list = new List<IHandle>(oldList);

            list.Remove(handler);

            _topics[topic] = list;
        }

        public void Publish<TMessage>(TMessage message)
             where TMessage : IMessage
        {
            IHandle handler;
            
            if (_topics.TryGetValue(typeof(TMessage).Name, out handler))
        {
                var typedHandler = handler as IHandle<TMessage>;
                typedHandler?.Handle(message);
            }
        }
    }
}
