using System;

namespace Challenge
{
    public interface IUserCollection
    {
        void Load(Tuple<string, string> nameTuple);
        int Count();
    }
}