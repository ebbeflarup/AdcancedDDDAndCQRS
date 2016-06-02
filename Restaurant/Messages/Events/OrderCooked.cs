namespace Restaurant.Messages.Events
{
    public class OrderCooked : MessageBase
    {
        public OrderCooked(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
