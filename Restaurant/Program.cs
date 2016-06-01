namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var waitor = new Waitor(new Cook(new AssistantManager(new Cashier(new OrderPrinter()))));
        }
    }
}
