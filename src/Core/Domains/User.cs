using System;
using System.Collections.Generic;

namespace NPress.Core.Domains
{
    public class User : IDataObject
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public DateTime RevisionDateTime { get; set; } = DateTime.UtcNow;
        public Enums.Role? Role { get; set; }
    }
}
