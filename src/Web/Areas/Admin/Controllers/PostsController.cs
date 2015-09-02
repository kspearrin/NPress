using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Core.Domains;
using NPress.Core.Services;
using NPress.Web.Areas.Admin.Models;

namespace NPress.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IPostService m_postService;

        public PostsController(IPostService postService)
        {
            m_postService = postService;
        }

        public async Task<IActionResult> Index(
            string before = null,
            int pageSize = 20)
        {
            var posts = await m_postService.PagePostsAsync(before, true, pageSize);
            var model = new PostIndexViewModel
            {
                Posts = PostViewModel.Build(posts),
                PageSize = pageSize,
                Before = posts.Count() == pageSize ? posts.LastOrDefault()?.Id : null
            };

            return View(model);
        }

        public IActionResult New()
        {
            return View(new NewPostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> New(NewPostViewModel model)
        {
            var post = new Post
            {
                Title = model.Title,
                Content = model.Content,
                UserId = "1"
            };

            await m_postService.CreatePostAsync(post);

            return View();
        }
    }
}
