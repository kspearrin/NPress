using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPress.Core.Domains;
using NPress.Core.Repositories;

namespace NPress.Core.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository m_blogRepository;

        public BlogService(
            IBlogRepository blogRepository)
        {
            m_blogRepository = blogRepository;
        }

        public async Task<Blog> GetBlogAsync()
        {
            return await m_blogRepository.GetAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            await m_blogRepository.ReplaceAsync(blog);
        }
    }
}
