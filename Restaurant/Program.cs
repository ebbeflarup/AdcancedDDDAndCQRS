using Newtonsoft.Json.Linq;

namespace Restaurant
{
    class Program
    {
        static void Main(string[] args)
        {
            var waitor = new Waitor(new Cook(new AssistantManager(new Cashier(new OrderPrinter()))));

            var lineItems = new LineItemList();
            lineItems.Add(new LineItem(2, "Ice Cream", 2.99));
            lineItems.Add(new LineItem(2, "Burger", 4.99));
            waitor.PlaceOrder(2, new LineItemList());
        }
    }
}
