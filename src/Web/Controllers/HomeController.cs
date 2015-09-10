using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using NPress.Core;
using NPress.Core.Repositories;
using NPress.Core.Services;
using NPress.Web.Models;

namespace NPress.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostService m_postService;

        public HomeController(
            IPostService postService)
        {
            m_postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await m_postService.PagePostsAsync(null, 1, 2, false);
            var model = new PagedPostsViewModel
            {
                Posts = PostViewModel.Build(posts),
                Page = 1,
                PageSize = 2,
                Cursor = posts.FirstOrDefault()?.Id
            };

            return View("~/Views/Posts/Index", model);
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}
