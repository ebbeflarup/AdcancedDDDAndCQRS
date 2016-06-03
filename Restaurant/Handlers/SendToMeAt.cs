using System;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class SendToMeAt
    {
        public DateTime When { get; }
        public IMessage Message { get; }

        public SendToMeAt(DateTime when, IMessage message)
        {
            When = when;
            Message = message;
        }
    }
}