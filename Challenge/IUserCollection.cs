using System;
using System.Collections;
using System.Collections.Generic;

namespace Challenge
{
    public interface IUserCollection : IEnumerable<string>
    {
        void Load(Tuple<string, string> nameTuple);
        void Load(string name);
        int Count();
        bool DoesExist(string name);
        void Clear();
        bool Remove(string name);
        List<string> GetFriendList(string name);
        void AddRange(IEnumerable<string> friendsList);
    }
}