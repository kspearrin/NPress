using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Repositories
{
    public interface IBlogRepository
    {
        Task<Blog> GetAsync();
        Task CreateAsync(Blog blog);
        Task ReplaceAsync(Blog blog);
    }
}
