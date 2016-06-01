using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class LineItem
    {
        private readonly JObject _jsonItem;

        public LineItem(int quantity, string item, double price) : this(new JObject
        {
            {"quantity", new JValue(quantity)},
            {"item", new JValue(item)},
            {"price", new JValue(price)}
        })
        {
        }

        public LineItem(JObject jsonItem)
        {
            _jsonItem = jsonItem;
        }

        public int quantity
        {
            get { return (int) _jsonItem.GetValue("quantity"); }
            set { _jsonItem.Property("quantity").Value = JToken.FromObject(value); }
        }

        public string item
        {
            get { return (string) _jsonItem.GetValue("item"); }
            set { _jsonItem.Property("item").Value = JToken.FromObject(value); }
        }

        public double price
        {
            get { return (double) _jsonItem.GetValue("price"); }
            set { _jsonItem.Property("price").Value = JToken.FromObject(value); }
        }
    }
}