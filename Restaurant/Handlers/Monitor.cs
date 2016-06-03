using System;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class Monitor : IHandle<IMessage>
    {
        public void Handle(IMessage message)
        {
            Console.WriteLine("Message received");
        }
    }
}
