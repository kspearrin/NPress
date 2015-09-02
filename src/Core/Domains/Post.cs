using System;

namespace NPress.Core.Domains
{
    public class Post : IDataObject
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; } = DateTime.UtcNow;
        public DateTime RevisionDateTime { get; set; } = DateTime.UtcNow;
    }
}
