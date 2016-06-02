using System.CodeDom;
using System.Collections.Generic;

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
        {
            _topics.Add(typeof(TMessage).Name, handler);
        }

        public void Publish<TMessage>(TMessage message)
        {
            IHandle handler;
            
            if (_topics.TryGetValue(typeof(TMessage).Name, out handler))
        {
                var typedHandler = (IHandle<TMessage>)handler;
                typedHandler.Handle(message);
            }
        }
    }
}
