using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Domains;

namespace NPress.Core.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<Post> GetBySlugAsync(string slug);
        Task<IEnumerable<Post>> PageAsync(string cursor, bool cursorBefore, int pageSize);
    }
}
