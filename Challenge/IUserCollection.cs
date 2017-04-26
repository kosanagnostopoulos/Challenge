using System;

namespace Challenge
{
    public interface IUserCollection
    {
        void Load(Tuple<string, string> nameTuple);
        void Load(string name);
        int Count();
        bool DoesExist(string name);
        void Clear();
        void Remove(string name);
    }
}