using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Restaurant.Messages;
using Restaurant.Messages.Commands;

namespace Restaurant
{
    public class TimedHandler<TMessage> : IHandle<SendToMeIn>, IStartable
    {
        private readonly IHandle<TMessage> _handler;
        private readonly IList<SendToMeAt> _sendToMeAts;
        private readonly object _sendToMeAtsLock;

        public TimedHandler(IHandle<TMessage> handler)
        {
            _handler = handler;
            _sendToMeAts = new List<SendToMeAt>();
            _sendToMeAtsLock = new object();
        }

        public void Handle(SendToMeIn sendToMeIn)
        {
            var sendToMeAt = new SendToMeAt(DateTime.Now.AddSeconds(sendToMeIn.Seconds), sendToMeIn.InnerMessage);

            lock (_sendToMeAtsLock)
            {
                _sendToMeAts.Add(sendToMeAt);
            }
        }

        public void Start()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    IList<SendToMeAt> sendNow;
                    lock (_sendToMeAtsLock)
                    {
                        sendNow = _sendToMeAts.Where(s => s.When > DateTime.Now).ToList();

                        foreach (var sendToMeAt in sendNow)
                        {
                            _sendToMeAts.Remove(sendToMeAt);
                        }
                    }

                    foreach (var sendToMeAt in sendNow)
                    {
                        _handler.Handle(sendToMeAt.Message);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }
    }
}
