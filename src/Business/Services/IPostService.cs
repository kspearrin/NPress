using System.Collections.Generic;
using System.Threading.Tasks;
using NPress.Core.Data;

namespace NPress.Business.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> PagePostsAsync(string cursor, bool beforeCursor, int pageSize, bool directionNext);
        Task CreatePostAsync(Post post);
    }
}
