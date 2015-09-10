﻿using System;
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
        public IActionResult Index()
        {
            return View();
        }

        [Route("{slug}")]
        public new async Task<IActionResult> View(string slug)
        {
            var post = await m_postService.GetPostBySlugAsync(slug);
            if(post == null)
            {
                // TODO: 404
            }

            return View(new PostViewModel(post));
        }
    }
}
