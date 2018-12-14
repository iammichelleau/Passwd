using System.Data.SqlTypes;

namespace PasswdApi.Models
{
    public class User
    {
        public string Name { get; }
        private string Password { get; }
        private int Uid { get; }
        private int Gid { get; }
        private string Comment { get; }
        private string Home  { get; }
        private string Shell { get; }

        public User(string name, string password, int uid, int gid, string comment, string home, string shell)
        {
            Name = name;
            Password = password;
            Uid = uid;
            Gid = gid;
            Comment = comment;
            Home = home;
            Shell = shell;
        }

        public bool Equals(int uid)
        {
            return (Uid == uid);
        }

        public bool Equals(string name, int? uid, int? gid, string comment, string home, string shell)
        {
            if (name != null && Name != name)
                return false;
            if (uid != null && Uid != uid)
                return false;
            if (gid != null && Gid != gid)
                return false;
            if (comment != null && Comment != comment)
                return false;
            if (home != null && Home != home)
                return false;
            if (shell != null && Shell != shell)
                return false;

            return true;
        }

        public string ToString()
        {
            return "{\"name\": \"" + Name + "\", \"uid\": \"" + Uid + "\", \"gid\": \"" + Gid + "\", \"comment\": \"" + Comment
                   + "\", \"home\": \"" + Home + "\", \"shell\": \"" + Shell + "\"}";
        }
    }
}
