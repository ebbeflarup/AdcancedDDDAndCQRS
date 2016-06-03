using System;
using Restaurant.Messages;

namespace Restaurant
{
    public class Monitor : IHandle<IMessage>
    {
        public void Handle(IMessage t)
        {
            Console.WriteLine("Message received");
        }
    }
}
