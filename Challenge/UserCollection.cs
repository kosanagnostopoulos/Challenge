﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class UserCollection : IUserCollection
    {
        private const int LETTERS_OF_ALPHABET = 26;
        private const int ASCII_OF_LETTER_A = 65;

        private List<string>[] _index = new List<string>[LETTERS_OF_ALPHABET];

        public UserCollection(ICollection<string> collection)
        {
            InitializeCollection();
        }

        private void InitializeCollection()
        {
            Clear();
        }

        public void Load(Tuple<string, string> nameTuple)
        {
            if (nameTuple == null)
            {
                throw new ArgumentNullException(nameof(nameTuple));
            }

            LoadSingleName(nameTuple.Item1);
            LoadSingleName(nameTuple.Item2);
        }

        private void LoadSingleName(string name)
        {

            if (name[0] < 'A' || name[0] > 'Z')
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            if (!_index[FindArrayBasedOnFirstLetter(name)].Contains(name))
            {
                _index[FindArrayBasedOnFirstLetter(name)].Add(name);
            }
        }

        private static int FindArrayBasedOnFirstLetter(string name)
        {
            return name[0] - ASCII_OF_LETTER_A;
        }

        public int Count()
        {
            int num = 0;
            foreach (var list in _index)
            {
                num += list.Count;
            }
            return num;
        }

        public bool DoesExist(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            return _index[FindArrayBasedOnFirstLetter(name)].Contains(name);
        }

        public void Load(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }
            LoadSingleName(name);
        }

        public void Clear()
        {
            for (int i = 0; i < _index.Length; ++i)
            {
                _index[i] = new List<string>();
            }
        }

        public bool Remove(string name)
        {
            return _index[FindArrayBasedOnFirstLetter(name)].Remove(name);
        }

        public IEnumerator<string> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public List<string> GetFriendList(string name)
        {
            throw new NotImplementedException();
        }


        public void AddRange(IEnumerable<string> friendsList)
        {
            if (friendsList == null)
            {
                throw new ArgumentNullException(nameof(friendsList));
            }
            foreach (var friend in friendsList)
            {
                LoadSingleName(friend);
            }
        }
    }
}