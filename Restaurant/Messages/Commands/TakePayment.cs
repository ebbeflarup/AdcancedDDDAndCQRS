namespace Restaurant.Messages.Commands
{
    public class TakePayment : MessageBase
    {
        public Order Order { get; private set; }

        public TakePayment(Order order)
        {
            Order = order;
        }
    }
}
