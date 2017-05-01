using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class DictionaryCollectionEnumerator : IEnumerator
    {
        private UserCollection[] _index;
        private UserCollectionEnumerator _enumerator;
        private int _position = -1;

        public DictionaryCollectionEnumerator(UserCollection[] index)
        {
            _index = index;
            Reset();
        }

        public bool MoveNext()
        {
            bool allMove = false;
            do
            {
                allMove = _enumerator.MoveNext();
                if (!allMove)
                {
                    ++_position;
                    if (_position < 26)
                    {
                        _enumerator = _index[_position].GetEnumerator();
                    }
                }

            } while (!allMove && _position < 26);
            return allMove;
        }

        public void Reset()
        {
            _position = 0;
            _enumerator = _index[_position].GetEnumerator();
        }

        public object Current
        {
            get
            {
                try
                {
                    return _enumerator.Current;
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
