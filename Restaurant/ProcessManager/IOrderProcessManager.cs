using System;
using Restaurant.Handlers;
using Restaurant.Messages.Events;

namespace Restaurant.ProcessManager
{
    public interface IOrderProcessManager : IHandle<OrderPlaced>
    {
         Action<Guid> OnCompleted { set; }
    }
}