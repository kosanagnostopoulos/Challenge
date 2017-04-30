using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge
{
    public class User
    {
        public string Name { get; set; }
        public IList<string> Friends { get; set; }

        public User(string name)
        {
            Name = name;
            Friends = new List<string>();
        }
        public User(string name, string friend) : this(name)
        {
            Friends.Add(friend);
        }

        public void AddFriend(string name)
        {
            if (!Friends.Contains(name))
            {
                Friends.Add(name);
            }
        }
    }
}
