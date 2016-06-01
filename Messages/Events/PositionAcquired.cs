namespace Messages.Events
{
    public class PositionAcquired
    {
        public PositionAcquired(int price)
        {
            Price = price;
        }

        public int Price { get; }
    }
}
