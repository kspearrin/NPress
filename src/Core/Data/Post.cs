using System;

namespace NPress.Core.Data
{
    public class Post : IDataObject
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime RevisionDateTime { get; set; }
    }
}
