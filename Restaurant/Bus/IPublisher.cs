using Restaurant.Messages;

namespace Restaurant.Bus
{
    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
    }
}