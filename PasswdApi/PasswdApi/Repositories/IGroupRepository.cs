using System.Collections.Generic;
using PasswdApi.Models;

namespace PasswdApi.Repositories
{
    public interface IGroupRepository
    {
        Group Create(string name, string password, int gid, params string[] members);
        IEnumerable<Group> Get();
        IEnumerable<Group> Get(string name, int? gid, params string[] members);
        Group GetById(int uid);
    }
}
