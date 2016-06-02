namespace Restaurant.Messages.Events
{
    public class OrderPlaced : MessageBase
    {
        public OrderPlaced(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
