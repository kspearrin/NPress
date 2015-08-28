using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NPress.Data.Models
{
    public class Post : IDataModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
