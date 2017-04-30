using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class UserCollectionEnumerator : IEnumerator
    {
        protected const int ELEMENT_RESET_POSITION = -1;
        protected const int ELEMENT_FIRST_POSITION = 0;
        protected const int ARRAY_RESET_POSITION = 0;

        protected readonly List<User>[] _index;

        protected int _arrayPointer = ARRAY_RESET_POSITION;
        protected int _elementPointer = ELEMENT_RESET_POSITION;

        public UserCollectionEnumerator(List<User>[] index)
        {
            _index = index;
        }

        public bool MoveNext()
        {
            ++_elementPointer;
            if (!WithinhTheBoundsOfCurrentArray())
            {
                GoToStartOfNextArray();
            }
            return (WithinTheBoundsOfIndex()) && (WithinhTheBoundsOfCurrentArray());
        }

        private void GoToStartOfNextArray()
        {
            ++_arrayPointer;
            _elementPointer = ELEMENT_FIRST_POSITION;
        }

        private bool WithinhTheBoundsOfCurrentArray()
        {
            return _elementPointer < _index[_arrayPointer].Count;
        }

        private bool WithinTheBoundsOfIndex()
        {
            return _arrayPointer < _index.Length;
        }

        public void Reset()
        {
            _arrayPointer = ARRAY_RESET_POSITION;
            _elementPointer = ELEMENT_RESET_POSITION;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _index[_arrayPointer][_elementPointer];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
