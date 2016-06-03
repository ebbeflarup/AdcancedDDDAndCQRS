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

        public Guid Id { get; private set; }

        public Guid CorrelationId { get; private set; }

        public Guid CausationId { get; private set; }
    }
}
