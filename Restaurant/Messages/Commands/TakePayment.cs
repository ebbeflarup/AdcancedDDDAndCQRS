namespace Restaurant.Messages.Commands
{
    public class TakePayment
    {
        public Order Order { get; private set; }

        public TakePayment(Order order)
        {
            Order = order;
        }
    }
}
