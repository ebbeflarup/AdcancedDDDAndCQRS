using System;
using Restaurant.Messages;

namespace Restaurant
{
    public interface ISubscriber
    {
        void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage;
        void Subscribe(Guid correlationId, IHandle handler);
    }
}