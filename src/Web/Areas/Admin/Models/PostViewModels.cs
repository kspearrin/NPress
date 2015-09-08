using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Web.Areas.Admin.Models
{
    public class PostIndexViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
        public int PageSize { get; set; }
        public string Before { get; set; }
    }

    public class PostViewModel
    {
        public PostViewModel() { }

        public PostViewModel(Post post)
        {
            Title = post.Title;
            Content = post.Content;
            Slug = post.Slug;
            Published = post.Published;
            PublishDateTime = post.PublishDateTime;
        }

        public string Title { get; set; }
        public string Content { get; set; }
        public string Slug { get; set; }
        public bool Published { get; set; } = true;
        public DateTime PublishDateTime { get; set; } = DateTime.UtcNow;

        public static IEnumerable<PostViewModel> Build(IEnumerable<Post> posts)
        {
            return posts?.Select(p => new PostViewModel(p));
        }
    }
}
