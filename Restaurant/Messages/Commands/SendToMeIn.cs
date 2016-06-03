using System;

namespace Restaurant.Messages.Commands
{
    public class SendToMeIn : MessageBase
    {
        public int Seconds { get; }
        public IMessage InnerMessage { get; }

        public SendToMeIn(int seconds, IMessage message, Guid correlationId, Guid causationId) : base(correlationId, causationId)
        {
            Seconds = seconds;
            InnerMessage = message;
        }
    }
}
