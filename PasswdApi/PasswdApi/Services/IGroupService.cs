using System.Collections.Generic;
using PasswdApi.Models;

namespace PasswdApi.Services
{
    public interface IGroupService
    {
        Group CreateGroup(string name, string password, int gid, string[] members);
        IEnumerable<Group> GetGroups();
        IEnumerable<Group> GetGroups(string name, int? gid, string[] members);
        Group GetGroupById(int gid);
    }
}
