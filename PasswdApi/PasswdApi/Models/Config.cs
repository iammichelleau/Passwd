using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using PasswdApi.Services;

namespace PasswdApi.Models
{
    public class Config
    {
        public readonly string PasswdFilePath = "\\passwd.txt";
        public readonly string GroupsFilePath = "\\groups.txt";

        private readonly IUserService _userService;
        private readonly IGroupService _groupService;

        public string Path { get; set; }

        public Config(IUserService userService, IGroupService groupService, string path)
        {
            _userService = userService;
            _groupService = groupService;
            Path = path;
        }

        public bool ValidateAndSave()
        {
            string line;
            var passwdFile = new StreamReader(Path + PasswdFilePath);
            var groupsFile = new StreamReader(Path + GroupsFilePath);

            while ((line = passwdFile.ReadLine()) != null)
            {
                if (!ValidatePasswdFile(line, PasswdFilePath))
                    return false;
            }

            while ((line = groupsFile.ReadLine()) != null)
            {
                if (!ValidateGroupsFile(line, GroupsFilePath))
                    return false;
            }

            return true;
        }

        public bool ValidatePasswdFile(string line, string filePath)
        {
            List<string> items = line.Split(":").ToList<string>();
            Regex regex = new Regex("[0-9]");

            if (items.Count == 7 && regex.IsMatch(items[2]) && regex.IsMatch(items[3]))
            {
                _userService.CreateUser(items[0], items[1], int.Parse(items[2]), int.Parse(items[3]), 
                    items[4], items[5], items[6]);
                return true;
            }

            return false;
        }

        public bool ValidateGroupsFile(string line, string filePath)
        {
            List<string> items = line.Split(":").ToList<string>();
            Regex regex = new Regex("[0-9]");

            if (items.Count == 4 && regex.IsMatch(items[2]))
            {
                _groupService.CreateGroup(items[0], items[1], int.Parse(items[2]), items[3].Split(","));
                return true;
            }

            return false;
        }
    }
}
