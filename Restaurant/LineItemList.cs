using System.Collections;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class LineItemList
    {
        private readonly IList _jsonList;

        public LineItemList(IList jsonList)
        {
            _jsonList = jsonList;
        }

        public LineItem this[int index] => new LineItem((JObject)_jsonList[index]);

        public void Add(LineItem lineItem)
        {
            _jsonList.Add(lineItem);
        }
    }
}