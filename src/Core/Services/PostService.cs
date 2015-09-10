using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;
using NPress.Core.Repositories;

namespace NPress.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository m_postRepository;

        public PostService(
            IPostRepository postRepository)
        {
            m_postRepository = postRepository;
        }

        public async Task<Post> GetPostByIdAsync(string id)
        {
            return await m_postRepository.GetByIdAsync(id);
        }

        public async Task<Post> GetPostBySlugAsync(string slug)
        {
            return await m_postRepository.GetBySlugAsync(slug);
        }

        public async Task<IEnumerable<Post>> PagePostsAsync(string cursor, bool beforeCursor, int pageSize)
        {
            if(pageSize > 100)
            {
                pageSize = 100;
            }

            var posts = await m_postRepository.PageAsync(cursor, beforeCursor, pageSize);
            return posts;
        }

        public async Task CreatePostAsync(Post post)
        {
            await m_postRepository.CreateAsync(post);
        }
    }
}
