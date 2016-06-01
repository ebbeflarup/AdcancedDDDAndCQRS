namespace Messages.Events
{
    public class PriceUpdated
    {
        public PriceUpdated(int price)
        {
            Price = price;
        }

        public int Price { get; }
    }
}
