using System;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class Monitor : IHandle<IMessage>
    {
        public void Handle(IMessage orderPlaced)
        {
            Console.WriteLine("Message received");
        }
    }
}
