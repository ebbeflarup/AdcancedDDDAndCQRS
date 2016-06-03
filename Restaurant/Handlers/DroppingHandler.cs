using System;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class DroppingHandler<TMessage> : IHandle<TMessage> where TMessage : IMessage
    {
        private readonly IHandle<TMessage> _handler;
        private readonly Random _rand;

        public DroppingHandler(IHandle<TMessage> handler)
        {
            _handler = handler;
            _rand = new Random();
        }

        public void Handle(TMessage message)
        {
            if (_rand.Next(0, 10) == 7)
            {
                Console.WriteLine("Oops...");
                return;
            }

            _handler.Handle(message);
        }
    }
}
