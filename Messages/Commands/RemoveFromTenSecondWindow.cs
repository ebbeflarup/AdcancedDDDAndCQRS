namespace Messages.Commands
{
    public class RemoveFromTenSecondWindow
    {
        public int Price { get; private set; }
        public RemoveFromTenSecondWindow(int price)
        {
            Price = price;
        }
    }
}
