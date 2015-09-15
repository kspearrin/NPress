using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Core.Services;
using NPress.Web.Models;

namespace NPress.Web.Controllers
{
    [Route("~/posts")]
    public class PostsController : Controller
    {
        private readonly IPostService m_postService;

        public PostsController(
            IPostService postService)
        {
            m_postService = postService;
        }

        [Route("")]
        public async Task<IActionResult> Index(
            string cursor = null,
            int page = 1,
            int pageSize = 20)
        {
            var posts = await m_postService.PagePostsAsync(cursor, page, pageSize, false);
            var model = new PagedPostsViewModel
            {
                Posts = PostViewModel.Build(posts),
                Page = page,
                PageSize = pageSize,
                Cursor = cursor ?? posts.FirstOrDefault()?.Id
            };

            return View(model);
        }

        [Route("{slug}")]
        public new async Task<IActionResult> View(string slug)
        {
            var post = await m_postService.GetPostBySlugAsync(slug);
            if(post == null || !post.Published || post.PublishDateTime < DateTime.UtcNow)
            {
                // TODO: 404
            }

            return View(new PostViewModel(post));
        }
    }
}
