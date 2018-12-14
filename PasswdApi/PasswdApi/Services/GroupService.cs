using System.Collections.Generic;
using System.Linq;
using PasswdApi.Exceptions;
using PasswdApi.Models;
using PasswdApi.Repositories;

namespace PasswdApi.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _repository;

        public GroupService(IGroupRepository repository)
        {
            _repository = repository;
        }

        public Group CreateGroup(string name, string password, int gid, params string[] members)
        {
            return _repository.Create(name, password, gid, members);
        }

        public IEnumerable<Group> GetGroups()
        {
            var groups = _repository.Get();

            if (groups != null && groups.Any())
                return groups;

            throw new EntityNotFoundException();
        }

        public IEnumerable<Group> GetGroups(string name, int? gid, params string[] members)
        {
            var groups = _repository.Get(name, gid, members);

            if (groups != null && groups.Any())
                return groups;

            throw new EntityNotFoundException();
        }

        public Group GetGroupById(int uid)
        {
            var group = _repository.GetById(uid);

            if (group != null)
            {
                return group;
            }

            throw new EntityNotFoundException();
        }
    }
}
