using System;
using Restaurant;
using Xunit;

namespace UnitTests
{
    public class DocumentTests
    {
        [Fact]
        public void DeserializeAddDataSerialize()
        {
            var json = @"{
                tableNumber : 23,
                lineItems : [{
                    quantity : 2,
                    item : ""razor blade ice cream"",
                    price : 2.99
                }],
                tax : 3.00,
                total : 3.00,
                paid : false,
                ingredients : ""razor blades, ice cream""
            }";

            var order = new Order(json);

            Assert.Equal(23, order.TableNumber);
            Assert.Equal(2, order.LineItems[0].quantity);
            Assert.Equal("razor blade ice cream", order.LineItems[0].item);
            Assert.Equal(2.99, order.LineItems[0].price);
            Assert.Equal(3.00, order.Tax);
            Assert.Equal(3.00, order.Total);
            Assert.Equal(false, order.Paid);
            Assert.Equal("razor blades, ice cream", order.Ingredients);

            order.LineItems[0].quantity = 3;
            Assert.Equal(3, order.LineItems[0].quantity);

            order.LineItems.Add(new LineItem(5, "bloody mary", 1.95));
        }

        [Fact]
        public void CreateNewOrder()
        {
            var list = new LineItemList();

            list.Add(new LineItem(3, "pony marmelade", 9.45));

            var order = new Order(Guid.NewGuid(), 12, list);

            Assert.Equal(12, order.TableNumber);
        }
    }
}
