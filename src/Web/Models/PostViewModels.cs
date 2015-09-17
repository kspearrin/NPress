using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Web.Models
{
    public class PagedPostsViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; } = new List<PostViewModel>();
        public string Cursor { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class PostViewModel
    {
        public PostViewModel() { }

        public PostViewModel(Post post)
        {
            if(post == null)
            {
                throw new ArgumentNullException(nameof(post));
            }

            Id = post.Id;
            Title = post.Title;
            Content = post.Content;
            Slug = post.Slug;
            Published = post.Published;
            PublishDateTime = post.PublishDateTime;
        }

        public string Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Slug { get; set; }
        [Required]
        public bool Published { get; set; } = true;
        [Required]
        public DateTime PublishDateTime { get; set; } = DateTime.UtcNow;

        public static IEnumerable<PostViewModel> Build(IEnumerable<Post> posts)
        {
            return posts?.Select(p => new PostViewModel(p));
        }
    }
}
