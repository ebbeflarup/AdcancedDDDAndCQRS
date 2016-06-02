namespace Restaurant.Messages.Events
{
    public class OrderPaid : MessageBase
    {
        public OrderPaid(Order order)
        {
            Order = order;
        }

        public Order Order { get; }
    }
}
