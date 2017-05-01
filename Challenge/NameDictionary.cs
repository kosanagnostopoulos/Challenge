using System;
using System.Collections;
using System.Collections.Generic;

namespace Challenge
{
    public class NameDictionary : IUserCollection
    {
        private const int LETTERS_OF_ALPHABET = 26;
        private const int ASCII_OF_LETTER_A = 65;

        public NameDictionary()
        {
            InitializeCollection();
        }

        private static int FindArrayBasedOnFirstLetterOfSurname(string name)
        {
            var names = name.Split('_');
            return names[1][0] - ASCII_OF_LETTER_A;
        }

        private void InitializeCollection()
        {
            Clear();
        }

        private UserCollection[] _index = new UserCollection[LETTERS_OF_ALPHABET];

        public IEnumerator GetEnumerator()
        {
            return new DictionaryCollectionEnumerator(_index);
        }

        public void Load(Tuple<string, string> nameTuple)
        {
            _index[FindArrayBasedOnFirstLetterOfSurname(nameTuple.Item1)].LoadFriendPair(nameTuple.Item1 , nameTuple.Item2);
            _index[FindArrayBasedOnFirstLetterOfSurname(nameTuple.Item2)].LoadFriendPair(nameTuple.Item2, nameTuple.Item1);
        }

        public void Load(string name)
        {
            _index[FindArrayBasedOnFirstLetterOfSurname(name)].Load(name);
        }

        public int Count()
        {
            int num = 0;
            foreach (var userCollection in _index)
            {
                num += userCollection.Count();
            }
            return num;
        }

        public bool DoesExist(string name)
        {
            return _index[FindArrayBasedOnFirstLetterOfSurname(name)].DoesExist(name);
        }

        public void Clear()
        {
            for (int i = 0; i < LETTERS_OF_ALPHABET; i++)
            {
                _index[i] = new UserCollection();
            }
        }

        public bool Remove(string name)
        {
            return _index[FindArrayBasedOnFirstLetterOfSurname(name)].Remove(name);
        }

        public IEnumerable<string> GetFriendList(string name)
        {
            return _index[FindArrayBasedOnFirstLetterOfSurname(name)].GetFriendList(name);
        }

        public void AddRange(IEnumerable<string> friendsList)
        {
            if (friendsList == null)
            {
                throw new ArgumentNullException(nameof(friendsList));
            }
            foreach (var friend in friendsList)
            {
                _index[FindArrayBasedOnFirstLetterOfSurname(friend)].LoadSingleName(friend);
            }
        }
    }
}