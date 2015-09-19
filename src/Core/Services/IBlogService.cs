using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Services
{
    public interface IBlogService
    {
        Task<Blog> GetBlogAsync();
        Task UpdateBlogAsync(Blog blog);
    }
}