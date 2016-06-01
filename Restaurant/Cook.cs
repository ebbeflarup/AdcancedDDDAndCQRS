using System.Threading;

namespace Restaurant
{
    public class Cook : IHandleOrder
    {
        private readonly IHandleOrder _orderHandler;

        public Cook(IHandleOrder orderHandler)
        {
            _orderHandler = orderHandler;
        }

        public void Handle(Order order)
        {
            order.Ingredients = "ponies, elephants";
            Thread.Sleep(2000);

            _orderHandler.Handle(order);
        }
    }
}
