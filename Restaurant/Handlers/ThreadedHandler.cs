using System.Collections.Concurrent;
using System.Threading;
using Restaurant.Messages;

namespace Restaurant.Handlers
{
    public class ThreadedHandler<TMessage> : IHandle<TMessage>, IStartable, IMonitorable where TMessage : IMessage
    {
        public string Name { get; }

        public int Count => _messages.Count;

        private readonly IHandle<TMessage> _handler;
        private readonly ConcurrentQueue<TMessage> _messages; 

        public ThreadedHandler(IHandle<TMessage> handler, string name)
        {
            Name = name;
            _handler = handler;
            _messages = new ConcurrentQueue<TMessage>();
        }

        public void Start()
        {
            var thread = new Thread(() =>
            {
                while (true)
                {
                    TMessage order;
                    while (_messages.TryDequeue(out order))
                    {
                        _handler.Handle(order);
                    }
                    Thread.Sleep(1);
                }
            });
            thread.Start();
        }

        public void Handle(TMessage message)
        {
            _messages.Enqueue(message);
        }
    }
}
