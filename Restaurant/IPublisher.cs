using Restaurant.Messages;

namespace Restaurant
{
    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }
}