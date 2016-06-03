using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Restaurant.Model
{
    public class LineItem
    {
        private readonly JObject _jsonItem;

        public LineItem(int quantity, string item, double price) : this(new JObject
        {
            {"quantity", new JValue(quantity)},
            {"item", new JValue(item)},
            {"price", new JValue(price)}
        }) { }

        public LineItem(JObject jsonItem)
        {
            _jsonItem = jsonItem;
        }

        [JsonProperty(PropertyName = "quantity")]
        public int Quantity
        {
            get { return (int) _jsonItem.GetValue("quantity"); }
            set { _jsonItem.Property("quantity").Value = JToken.FromObject(value); }
        }

        [JsonProperty(PropertyName = "item")]
        public string Item
        {
            get { return (string) _jsonItem.GetValue("item"); }
            set { _jsonItem.Property("item").Value = JToken.FromObject(value); }
        }

        [JsonProperty(PropertyName = "price")]
        public double Price
        {
            get { return (double) _jsonItem.GetValue("price"); }
            set { _jsonItem.Property("price").Value = JToken.FromObject(value); }
        }
    }
}