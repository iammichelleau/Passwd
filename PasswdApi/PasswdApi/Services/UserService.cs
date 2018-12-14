using System.Collections.Generic;
using System.Linq;
using PasswdApi.Exceptions;
using PasswdApi.Models;
using PasswdApi.Repositories;

namespace PasswdApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public User CreateUser(string name, string password, int uid, int gid, string comment, string home, string shell)
        {
            return _repository.Create(name, password, uid, gid, comment, home, shell);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _repository.Get();

            if (users != null && users.Any())
                return users;

            throw new EntityNotFoundException();
        }

        public IEnumerable<User> GetUsers(string name, int? uid, int? gid, string comment, string home, string shell)
        {
            var users = _repository.Get(name, uid, gid, comment, home, shell);

            if (users != null && users.Any())
                return users;

            throw new EntityNotFoundException();
        }

        public User GetUserById(int uid)
        {
            var user = _repository.GetById(uid);

            if (user != null)
            {
                return user;
            }

            throw new EntityNotFoundException();
        }
    }
}
