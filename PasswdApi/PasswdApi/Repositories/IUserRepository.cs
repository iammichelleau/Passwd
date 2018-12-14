using System.Collections.Generic;
using PasswdApi.Models;

namespace PasswdApi.Repositories
{
    public interface IUserRepository
    {
        User Create(string name, string password, int uid, int gid, string comment, string home, string shell);
        IEnumerable<User> Get();
        IEnumerable<User> Get(string name, int? uid, int? gid, string comment, string home, string shell);
        User GetById(int uid);
    }
}
