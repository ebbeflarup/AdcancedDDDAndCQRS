using System;
using Restaurant.Handlers;
using Restaurant.Messages;

namespace Restaurant.Bus
{
    public interface ISubscriber
    {
        void Subscribe<TMessage>(IHandle<TMessage> handler) where TMessage : IMessage;
        void Subscribe<TMessage>(Guid correlationId, IHandle<TMessage> handler) where TMessage : IMessage;
    }
}