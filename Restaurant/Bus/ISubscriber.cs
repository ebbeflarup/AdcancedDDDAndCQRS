using System;
using Restaurant.Handlers;
using Restaurant.Messages;

namespace Restaurant.Bus
{
    public interface ISubscriber
    {
        void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage;
        void Subscribe(Guid correlationId, IHandle handler);
    }
}