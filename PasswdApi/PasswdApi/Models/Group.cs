using System.Collections.Generic;
using System.Linq;

namespace PasswdApi.Models
{
    public class Group
    {
        private string Name { get; }
        private string Password { get; }
        private int Gid { get; }
        private IEnumerable<string> Members { get; }

        public Group(string name, string password, int gid, params string[] members)
        {
            Name = name;
            Password = password;
            Gid = gid;
            Members = members;
        }

        public bool Equals(int gid)
        {
            return (Gid == gid);
        }

        public bool Equals(string name, int? gid, params string[] members)
        {
            if (name != null && Name != name)
                return false;
            if (gid != null && Gid != gid)
                return false;
            if (members != null)
            {
                var contains = true;

                foreach (var member in members)
                {
                    if (!Members.Contains(member))
                        return false;
                }
            }

            return true;
        }

        public string ToString()
        {
            return "{\"name\": \"" + Name + "\", \"gid\": \"" + Gid + "\", \"members\": \"" + 
                   string.Join(", ", Members.Select(x => x.ToString())) + "\"}";
        }
    }
}
