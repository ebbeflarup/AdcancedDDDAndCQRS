using System;

namespace Restaurant.Messages
{
    public class MessageBase : IMessage
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
