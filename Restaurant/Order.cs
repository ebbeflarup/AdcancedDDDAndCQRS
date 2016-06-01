using System.Collections;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class Order
    {
        private readonly JObject _order;

        public Order(string json)
        {
            _order = JObject.Parse(json);
            LineItems = new LineItemList(_order.GetValue("lineItems") as JArray);
        }

        public int TableNumber
        {
            get { return (int) _order.GetValue("tableNumber"); }
            set { _order.Property("tableNumber").Value = value; }
        }

        public LineItemList LineItems { get; }

        public double Tax
        {
            get { return (double)_order.GetValue("tax"); }
            set { _order.Property("tax").Value = value; }
        }

        public double Total
        {
            get { return (double)_order.GetValue("total"); }
            set { _order.Property("total").Value = value; }
        }

        public bool Paid
        {
            get { return (bool)_order.GetValue("paid"); }
            set { _order.Property("paid").Value = value; }
        }

        public string Ingredients
        {
            get { return (string)_order.GetValue("ingredients"); }
            set { _order.Property("ingredients").Value = value; }
        }
    }
}