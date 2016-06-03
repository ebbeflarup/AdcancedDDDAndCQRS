namespace Restaurant.Messages.Commands
{
    public class PriceOrder : MessageBase
    {
        public Order Order { get; private set; }

        public PriceOrder(Order order)
        {
            Order = order;
        }
    }
}
