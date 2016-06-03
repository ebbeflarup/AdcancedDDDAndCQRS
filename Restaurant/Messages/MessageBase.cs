using System;

namespace Restaurant.Messages
{
    public class MessageBase : IMessage
    {
        public MessageBase(Guid correlationId, Guid causationId)
        {
            CorrelationId = correlationId;
            CausationId = causationId;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public Guid CorrelationId { get; }

        public Guid CausationId { get; }
    }
}
