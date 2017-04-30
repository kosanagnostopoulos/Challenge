using System;
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

        private List<User>[] _index = new List<User>[LETTERS_OF_ALPHABET];

        public UserCollection()
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
            LoadFriendPair(nameTuple.Item1, nameTuple.Item2);
            LoadFriendPair(nameTuple.Item2, nameTuple.Item1);
        }

        private void LoadFriendPair(string user, string friend)
        {
            if (user[0] < 'A' || user[0] > 'Z')
            {
                throw new ArgumentOutOfRangeException(nameof(user));
            }

            if (friend[0] < 'A' || friend[0] > 'Z')
            {
                throw new ArgumentOutOfRangeException(nameof(friend));
            }
            var existingUser = _index[FindArrayBasedOnFirstLetter(user)].Find(s => s.Name == user);
            if (existingUser == null)
            {
                _index[FindArrayBasedOnFirstLetter(user)].Add(new User(user, friend));
            }
            else
            {
                existingUser.AddFriend(friend);
            }
        }

        private static bool IsAlreadyStored(User existingUser)
        {
            return existingUser != null;
        }

        private void LoadSingleName(string name)
        {

            if (name[0] < 'A' || name[0] > 'Z')
            {
                throw new ArgumentOutOfRangeException(nameof(name));
            }

            if (!DoesExist(name))
            {
                _index[FindArrayBasedOnFirstLetter(name)].Add(new User(name));
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
            var a = _index[FindArrayBasedOnFirstLetter(name)].Find(s => s.Name == name);
            return _index[FindArrayBasedOnFirstLetter(name)].Find(s => s.Name == name) != null;
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
                _index[i] = new List<User>();
            }
        }

        public bool Remove(string name)
        {
            return _index[FindArrayBasedOnFirstLetter(name)].RemoveAll(s => s.Name == name) != 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public UserCollectionEnumerator GetEnumerator()
        {
            return new UserCollectionEnumerator(_index);
        }

        public User Find(string name)
        {
            var user = _index[FindArrayBasedOnFirstLetter(name)].Find(s => s.Name == name);
            if (user == null)
            {
                throw new KeyNotFoundException(nameof(name));
            }
            return user;
        }

        public IEnumerable<string> GetFriendList(string name)
        {
            return Find(name).Friends;
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