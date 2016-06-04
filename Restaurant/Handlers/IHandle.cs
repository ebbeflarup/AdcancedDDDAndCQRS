using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public interface IHandle<in TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
