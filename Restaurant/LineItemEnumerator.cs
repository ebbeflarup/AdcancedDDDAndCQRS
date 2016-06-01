using System.Collections;
using System.Collections.Generic;

namespace Restaurant
{
    public class LineItemEnumerator : IEnumerator<LineItem>
    {
        private readonly LineItemList _lineItemList;
        private int _index = -1;

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

        object IEnumerator.Current => Current;

        public LineItem Current => _lineItemList[_index];
        public void Dispose()
        {
        }
    }
}