using System.Collections.Generic;
using System.Linq;
using PasswdApi.Models;

namespace PasswdApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> users = new List<User>();

        public User Create(string name, string password, int uid, int gid, string comment, string home, string shell)
        {
            var newUser = new User(name, password, uid, gid, comment, home, shell);
            users.Add(newUser);
            return newUser;
        }

        public IEnumerable<User> Get()
        {
            return users;
        }

        public IEnumerable<User> Get(string name, int? uid, int? gid, string comment, string home, string shell)
        {
            return users.Where(x => x.Equals(name, uid, gid, comment, home, shell));
        }

        public User GetById(int uid)
        {
            return users.FirstOrDefault(x => x.Equals(uid));
        }
    }
}
