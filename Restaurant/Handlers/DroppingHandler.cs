using System;

namespace Restaurant.Handlers
{
    public class DroppingHandler<TMessage> : IHandle<TMessage>
    {
        private readonly IHandle<TMessage> _handler;
        private readonly Random _rand;

        public DroppingHandler(IHandle<TMessage> handler)
        {
            _handler = handler;
            _rand = new Random();
        }

        public void Handle(TMessage orderPlaced)
        {
            if (_rand.Next(0, 10) == 7)
            {
                Console.WriteLine("Oops...");
                return;
            }

            _handler.Handle(orderPlaced);
        }
    }
}
