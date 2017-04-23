using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class UserCollection
    {
        private readonly ICollection<string> _collection;

        public UserCollection(ICollection<string> collection)
        {
            _collection = collection;
        }

        public void Load(Tuple<string, string> nameTuple)
        {
            LoadSingleName(nameTuple.Item1);
            LoadSingleName(nameTuple.Item2);
        }

        private void LoadSingleName(string name)
        {
            if (!_collection.Contains(name))
            {
                _collection.Add(name);
            }
        }

        public int Count()
        {
            return _collection.Count();
        }
    }
}