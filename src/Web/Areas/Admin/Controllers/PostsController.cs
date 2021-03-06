﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using NPress.Core.Domains;
using NPress.Core.Services;
using NPress.Web.Models;

namespace NPress.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("~/admin/posts")]
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IPostService m_postService;

        public PostsController(IPostService postService)
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

        [Route("new")]
        public IActionResult New()
        {
            ViewBag.Title = "New Post";

            return View("NewEdit", new PostViewModel());
        }

        [HttpPost("new")]
        public async Task<IActionResult> New(PostViewModel model)
        {
            ViewBag.Title = "New Post";

            if(ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    Content = model.Content,
                    UserId = "1",
                    Slug = model.Slug,
                    Published = model.Published,
                    PublishDateTime = model.PublishDateTime
                };

                await m_postService.CreatePostAsync(post);
            }

            return View("NewEdit", model);
        }

        [Route("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var post = await m_postService.GetPostByIdAsync(id);
            if(post == null)
            {
                // TODO: not found
            }

            ViewBag.Title = $"Edit {post.Title}";

            return View("NewEdit", new PostViewModel(post));
        }

        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit(string id, PostViewModel model)
        {
            var post = await m_postService.GetPostByIdAsync(id);
            if(post == null)
            {
                // TODO: not found
            }

            ViewBag.Title = $"Edit {post.Title}";

            if(ModelState.IsValid)
            {
                post.Title = model.Title;
                post.Content = model.Content;
                post.Slug = model.Slug;
                post.Published = model.Published;
                post.PublishDateTime = model.PublishDateTime;

                await m_postService.UpdatePostAsync(post);
            }

            return View("NewEdit", model);
        }
    }
}
