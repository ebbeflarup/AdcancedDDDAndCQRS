using System;

namespace Restaurant.Messages
{
    public interface IMessage
    {
        Guid Id { get; }
        Guid CorrelationId { get; }
        Guid CausationId { get; }
    }
}