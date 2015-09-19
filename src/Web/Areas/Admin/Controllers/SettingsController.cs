using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NPress.Core.Services;
using NPress.Web.Models;

namespace NPress.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("~/admin/settings")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly IBlogService m_blogService;

        public SettingsController(IBlogService blogService)
        {
            m_blogService = blogService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var blog = await m_blogService.GetBlogAsync();
            var model = new SettingsViewModel
            {
                Blog = new BlogViewModel(blog)
            };

            return View(model);
        }

        [HttpPost("")]
        public async Task<IActionResult> Index(SettingsViewModel model)
        {
            if(ModelState.IsValid)
            {
                var blog = await m_blogService.GetBlogAsync();
                blog.Title = model.Blog.Title;

                await m_blogService.UpdateBlogAsync(blog);
            }

            return View(model);
        }
    }
}
