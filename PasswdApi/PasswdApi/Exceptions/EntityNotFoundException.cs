using System;

namespace PasswdApi.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base("Entity Not Found")
        {
        }
    }
}
