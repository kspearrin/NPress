using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Data;

namespace NPress.Web.Areas.Admin.Models
{
    public class PostIndexViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
    }

    public class NewPostViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
