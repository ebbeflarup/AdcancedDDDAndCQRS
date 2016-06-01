using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Restaurant
{
    public class LineItemList : IEnumerable<LineItem>
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

        public IEnumerator<LineItem> GetEnumerator()
        {
            return new LineItemEnumerator(this);
        }

        public int Count => _jsonList.Count;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}