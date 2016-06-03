using System;
using System.Collections.Generic;
using System.Linq;
using Restaurant.Messages;
using Xunit;

namespace UnitTests
{
    public class GenericTests
    {
        [Fact]
        public void CanInputConcreteTypeInGenericList()
        {

        }
    }

    public interface IHandle<in TMessage>
        where TMessage : IMessage
    {
        void Handle(TMessage message);
    }

    public interface ISubscriber
    {
        void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage;
        void Subscribe(Guid correlationId, IHandle<IMessage> handler);
    }

    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }

    public class Bus
    {
        private readonly IDictionary<string, IList<IHandle<IMessage>>> _topics;
        private readonly object _topicsLock = new object();

        public Bus()
        {
            _topics = new Dictionary<string, IList<IHandle<IMessage>>>();
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
            IList<IHandle<IMessage>> handlers;

            if (!_topics.TryGetValue(topic, out handlers)) return;

            foreach (var typedHandler in handlers.Select(handler => handler as IHandle<TMessage>))
            {
                typedHandler?.Handle(message);
            }
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler)
            where TMessage : IMessage
        {
            Subscribe(typeof(TMessage).Name, handler);
        }

        private void Subscribe<TMessage>(string topic, IHandle<TMessage> handler) where TMessage : IMessage
        {
            dynamic h = handler;

            lock (_topicsLock)
            {
                IList<IHandle<IMessage>> list = _topics.TryGetValue(topic, out list) ? new List<IHandle<IMessage>>(list) : new List<IHandle<IMessage>>();

                list.Add(h);

                _topics[topic] = list;
            }
        }

        public void Subscribe(Guid correlationId, IHandle<IMessage> handler)
        {
            Subscribe(correlationId.ToString(), handler);
        }

        public void Unsubscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage
        {
            Unsubscribe(typeof(TMessage).Name, handler);
        }

        public void Unsubscribe(Guid correlationId, IHandle<IMessage> handler)
        {
            Unsubscribe(correlationId.ToString(), handler);
        }

        private void Unsubscribe<TMessage>(string topic, IHandle<TMessage> handler) where TMessage : IMessage
        {
            dynamic h = handler;

            lock (_topicsLock)
            {
                var oldList = _topics[topic] ?? new List<IHandle<IMessage>>();
                var list = new List<IHandle<IMessage>>(oldList);

                list.Remove(h);

                _topics[topic] = list;
            }
        }
    }
}
