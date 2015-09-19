using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Web.Models
{
    public class BlogViewModel
    {
        public BlogViewModel() { }

        public BlogViewModel(Blog blog)
        {
            Title = blog.Title;
        }

        [Required]
        public string Title { get; set; }
    }

    public class SettingsViewModel
    {
        public BlogViewModel Blog { get; set; }
    }
}
