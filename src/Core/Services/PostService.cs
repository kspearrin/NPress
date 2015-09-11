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

        public async Task<IEnumerable<Post>> PagePostsAsync(string cursor, int page, int pageSize, bool ascending)
        {
            if(page < 1)
            {
                page = 0;
            }

            if(pageSize > 100)
            {
                pageSize = 100;
            }
            else if(pageSize < 0)
            {
                pageSize = 0;
            }

            var posts = await m_postRepository.PageAsync(cursor, page, pageSize, ascending);
            return posts;
        }

        public async Task CreatePostAsync(Post post)
        {
            if(!string.IsNullOrWhiteSpace(post.Id))
            {
                // TODO: Error
            }

            await m_postRepository.CreateAsync(post);
        }

        public async Task UpdatePostAsync(Post post)
        {
            if(string.IsNullOrWhiteSpace(post.Id))
            {
                // TODO: Error
            }

            await m_postRepository.ReplaceAsync(post);
        }
    }
}
