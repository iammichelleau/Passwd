using System.Collections.Generic;
using PasswdApi.Models;

namespace PasswdApi.Services
{
    public interface IUserService
    {
        User CreateUser(string name, string password, int uid, int gid, string comment, string home, string shell);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(string name, int? uid, int? gid, string comment, string home, string shell);
        User GetUserById(int uid);
    }
}
