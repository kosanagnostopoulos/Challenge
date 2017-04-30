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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            User obj1 = obj as User;
            if (obj1 == null) return false;
            return string.Equals(Name, obj1.Name);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }
    }
}
