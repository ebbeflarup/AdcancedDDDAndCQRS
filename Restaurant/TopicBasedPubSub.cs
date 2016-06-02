using System.Collections.Generic;
using Restaurant.Messages;

namespace Restaurant
{
    public class TopicBasedPubSub : IPublisher
    {
        private readonly IDictionary<string, IHandle> _topics;

        public TopicBasedPubSub()
        {
            _topics = new Dictionary<string, IHandle>();
        }

        public void Subscribe<TMessage>(IHandle<TMessage> handler)
            where TMessage : IMessage
        {
            _topics.Add(typeof(TMessage).Name, handler);
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
