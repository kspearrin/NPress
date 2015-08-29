using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Data;

namespace NPress.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> PageAsync(string cursor, bool cursorBefore, int pageSize);
    }
}
