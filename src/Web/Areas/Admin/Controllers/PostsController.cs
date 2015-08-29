using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Business.Services;
using NPress.Data.Repositories;
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
            string next = null,
            string previous = null,
            string first = null,
            int pageSize = 20)
        {
            var posts = await m_postService.PagePostsAsync(next ?? previous, previous == null, pageSize, previous == null);
            var model = new PostIndexViewModel
            {
                Posts = posts,
                PageSize = pageSize,
                Next = posts.Count() == pageSize ? posts.LastOrDefault()?.Id : null,
                Previous = first != posts.FirstOrDefault()?.Id && (next != null || previous != null) ? posts.FirstOrDefault()?.Id : null,
                First = first ?? posts.FirstOrDefault()?.Id
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
            var post = new Core.Data.Post
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
