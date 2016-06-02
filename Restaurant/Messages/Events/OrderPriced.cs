namespace Restaurant.Messages.Events
{
    public class OrderPriced : MessageBase
    {
        public OrderPriced(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
