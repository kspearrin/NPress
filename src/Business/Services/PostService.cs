using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Data;
using NPress.Data.Repositories;

namespace NPress.Business.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository m_postRepository;

        public PostService(
            IPostRepository postRepository)
        {
            m_postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> PagePostsAsync(string cursor, bool beforeCursor, int pageSize, bool directionNext)
        {
            if(pageSize > 100)
            {
                pageSize = 100;
            }

            var posts = await m_postRepository.PageAsync(cursor, beforeCursor, pageSize);

            if(!string.IsNullOrWhiteSpace(cursor) && !directionNext)
            {
                return posts.Reverse();
            }

            return posts;
        }

        public async Task CreatePostAsync(Post post)
        {
            post.CreationDateTime = post.RevisionDateTime = DateTime.UtcNow;
            await m_postRepository.CreateAsync(post);
        }
    }
}
