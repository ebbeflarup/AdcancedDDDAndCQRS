using System.Collections;

namespace Restaurant
{
    public class LineItemEnumerator : IEnumerator
    {
        private readonly LineItemList _lineItemList;
        private int _index;

        public LineItemEnumerator(LineItemList lineItemList)
        {
            _lineItemList = lineItemList;
        }

        public bool MoveNext()
        {
            _index++;
            return _index < _lineItemList.Count;
        }

        public void Reset()
        {
            _index = 0;
        }

        public object Current => _lineItemList[_index];
    }
}