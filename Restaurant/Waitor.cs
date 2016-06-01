namespace Restaurant
{
    public class Waitor
    {
        private readonly IHandleOrder _handleOrder;

        public Waitor(IHandleOrder handleOrder)
        {
            _handleOrder = handleOrder;
        }
    }
}
