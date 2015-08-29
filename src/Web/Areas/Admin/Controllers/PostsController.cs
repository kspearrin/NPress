using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Data.Repositories;
using NPress.Web.Areas.Admin.Models;

namespace NPress.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IPostRepository m_postRepository;

        public PostsController(IPostRepository postRepository)
        {
            m_postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await m_postRepository.PageAsync("0", false, 20);

            var model = new PostIndexViewModel
            {
                Posts = posts
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
            await m_postRepository.CreateAsync(new Core.Data.Post
            {
                Title = model.Title,
                Content = model.Content,
                UserId = "1",
                CreationDateTime = DateTime.UtcNow,
                RevisionDateTime = DateTime.UtcNow
            });

            return View();
        }
    }
}
