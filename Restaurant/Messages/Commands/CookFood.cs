namespace Restaurant.Messages.Commands
{
    public class CookFood
    {
        public Order Order { get; private set; }

        public CookFood(Order order)
        {
            Order = order;
        }
    }
}
