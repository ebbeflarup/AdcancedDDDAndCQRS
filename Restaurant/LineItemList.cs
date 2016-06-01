using System.Collections;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class LineItemList
    {
        private readonly JArray _jsonList;

        public LineItemList()
        {
            _jsonList = new JArray();
        }

        public LineItemList(JArray jsonList)
        {
            _jsonList = jsonList;
        }

        public LineItem this[int index] => new LineItem((JObject)_jsonList[index]);

        public void Add(LineItem lineItem)
        {
            _jsonList.Add(JToken.FromObject(lineItem));
        }
    }
}