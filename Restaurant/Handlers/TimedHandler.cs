﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Restaurant.Bus;
using Restaurant.Messages.Commands;

namespace Restaurant.Handlers
{
    public class TimedHandler : IHandle<SendToMeIn>, IStartable
    {
        private readonly IPublisher _publisher;
        private readonly IList<SendToMeAt> _sendToMeAts;
        private readonly object _sendToMeAtsLock;

        public TimedHandler(IPublisher publisher)
        {
            _publisher = publisher;
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
                        sendNow = _sendToMeAts.Where(s => s.When < DateTime.Now).ToList();

                        foreach (var sendToMeAt in sendNow)
                        {
                            _sendToMeAts.Remove(sendToMeAt);
                        }
                    }

                    foreach (dynamic sendToMeAt in sendNow)
                    {
                        _publisher.Publish(sendToMeAt.Message);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }
    }
}
