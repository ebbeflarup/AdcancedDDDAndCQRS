using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Messages;
using Restaurant.Messages.Commands;

namespace Restaurant
{
    public class TimedHandler : IHandle<SendToMeIn>
    {
        private ConcurrentQueue<SendToMeAt> _sendToMeAts;

        public TimedHandler()
        {
            _sendToMeAts = new ConcurrentQueue<SendToMeAt>();
        }

        public void Handle(SendToMeIn sendToMeIn)
        {
            var sendToMeAt = new SendToMeAt(DateTime.Now.AddSeconds(sendToMeIn.Seconds), sendToMeIn.Message);


        }
    }

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
