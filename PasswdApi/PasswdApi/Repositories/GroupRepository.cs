using System.Collections.Generic;
using System.Linq;
using PasswdApi.Models;

namespace PasswdApi.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private List<Group> groups = new List<Group>();

        public Group Create(string name, string password, int gid, params string[] members)
        {
            var newGroup = new Group(name, password, gid, members);
            groups.Add(newGroup);
            return newGroup;
        }

        public IEnumerable<Group> Get()
        {
            return groups;
        }

        public IEnumerable<Group> Get(string name, int? gid, params string[] members)
        {
            return groups.Where(x => x.Equals(name, gid, members));
        }

        public Group GetById(int gid)
        {
            return groups.FirstOrDefault(x => x.Equals(gid));
        }
    }
}
