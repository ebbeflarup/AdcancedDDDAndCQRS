using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public interface IHandle { }

    public interface IHandle<in TMessage> : IHandle where TMessage : IMessage
    {
        void Handle(TMessage message);
    }
}
