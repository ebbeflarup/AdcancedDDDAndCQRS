using System;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class Order
    {
        private readonly JObject _order;

        public Order(Guid id, int tableNumber, LineItemList list)
        {
            _order = new JObject();

            Id = id;
            TableNumber = tableNumber;
            LineItems = list;
        }

        public Order(string json)
        {
            _order = JObject.Parse(json);
        }

        public Guid Id
        {
            get { return (Guid)_order.GetValue("id"); }
            private set { SetProperty("id", value); }
        }


        public int TableNumber
        {
            get { return (int) _order.GetValue("tableNumber"); }
            set { SetProperty("tableNumber", value); }
        }

        public bool IsDodgy
        {
            get { return (bool)_order.GetValue("isDodgy"); }
            set { SetProperty("isDodgy", value); }
        }

        public LineItemList LineItems
        {
            get { return new LineItemList(_order.GetValue("lineItems") as JArray); }
            private set { SetProperty("lineItems", value); }
        }

        public double Tax
        {
            get { return (double)_order.GetValue("tax"); }
            set { SetProperty("tax", value); }
        }

        public double Total
        {
            get { return (double)_order.GetValue("total"); }
            set { SetProperty("total", value); }
        }

        public bool Paid
        {
            get { return (bool)_order.GetValue("paid"); }
            set { SetProperty("paid", value); }
        }

        public string Ingredients
        {
            get { return (string)_order.GetValue("ingredients"); }
            set { SetProperty("ingredients", value); }
        }

        public string Serialize()
        {
            return _order.ToString();
        }

        private void SetProperty(string propertyName, object value)
        {
            var property = _order.Property(propertyName);
            if (property != null)
            {
                property.Value = JToken.FromObject(value);
            }
            else
            {
                _order.Add(propertyName, JToken.FromObject(value));
            }
        }


    }
}