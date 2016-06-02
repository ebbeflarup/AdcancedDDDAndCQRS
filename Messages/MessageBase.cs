using System;

namespace Messages
{
    public class MessageBase
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
